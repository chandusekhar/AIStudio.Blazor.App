﻿using AIStudio.Common.DI.AOP;
using AIStudio.Util;
using Quartz.Impl.AdoJobStore;
using Simple.Common;
using SqlSugar;
using StackExchange.Redis;
using System.Dynamic;
using System.Linq.Dynamic.Core;

namespace AIStudio.Business
{
    public class DataRepeatValidateAttribute : BaseAOPAttribute
    {
        public DataRepeatValidateAttribute(string[] validateFields, string[] validateFieldNames, bool matchOr = true)
        {
            if (validateFields.Length != validateFieldNames.Length)
                throw new Exception("校验列与列描述信息不对应!");

            _matchOr = matchOr;
            for (int i = 0; i < validateFields.Length; i++)
            {
                _validateFields.Add(validateFields[i], validateFieldNames[i]);
            }
        }
        private bool _allData { get; }
        private bool _matchOr { get; }
        private Dictionary<string, string> _validateFields { get; } = new Dictionary<string, string>();

        public override async Task Befor(IAOPContext context)
        {
            Type entityType = context.Arguments[0].GetType();
            var data = context.Arguments[0];
            List<string> whereList = new List<string>();
            var properties = _validateFields
                .Where(x => !data.GetPropertyValue(x.Key).IsNullOrEmpty())
                .ToList(); 

            var q = context.InvocationTarget.GetType().GetMethod("GetIQueryableDynamic").Invoke(context.InvocationTarget, new object[] { }) as ISugarQueryable<dynamic>;

            var conModels = new List<IConditionalModel>();
            conModels.Add(new ConditionalModel() { FieldName = "Id", ConditionalType = ConditionalType.NoEqual, FieldValue = data.GetPropertyValue("Id")?.ToString() });
            var conditionalList = new List<KeyValuePair<WhereType, SqlSugar.ConditionalModel>>();
            foreach (var aProperty in properties)
            {
                conditionalList.Add(new KeyValuePair<WhereType, ConditionalModel>(_matchOr ? WhereType.Or : WhereType.And, new ConditionalModel() { FieldName = aProperty.Key, ConditionalType = ConditionalType.Equal, FieldValue = data.GetPropertyValue(aProperty.Key)?.ToString() }));
            }
            conModels.Add(new ConditionalCollections() { ConditionalList = conditionalList });            

            var list = q.Where(conModels).ToList();
            if (list.Count > 0)
            {
                var repeatList = properties
                    .Where(x => list.Any(y => !((IDictionary<String, Object>)y)[x.Key].IsNullOrEmpty()))
                    .Select(x => x.Value)
                    .ToList();

                throw AjaxResultException.Status403Forbidden($"{string.Join(_matchOr ? "或" : "与", repeatList)}已存在!");
            }

            await Task.CompletedTask;
        }
    }
}
