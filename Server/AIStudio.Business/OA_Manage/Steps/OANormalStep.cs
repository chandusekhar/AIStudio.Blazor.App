﻿using AIStudio.Entity.DTO.OA_Manage;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using AIStudio.Common.CurrentUser;

namespace AIStudio.Business.OA_Manage.Steps
{
    /// <summary>
    /// 普通节点，不带审批
    /// </summary>
    public class OANormalStep : StepBodyAsync
    {
        /// <summary>
        /// 节点触发
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //public override ExecutionResult Run(IStepExecutionContext context)
        //{
        //    GetStep(context);
        //    return ExecutionResult.Next();
        //}

        /// <summary>
        /// 节点触发
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            GetStep(context);
            return Task.FromResult(ExecutionResult.Next());
        }

        /// <summary>
        /// 获取步骤
        /// </summary>
        /// <param name="context"></param>
        protected void GetStep(IStepExecutionContext context)
        {
            if (context.Workflow.Data is OAData)
            {
                OAData oAData = context.Workflow.Data as OAData;

                var oAStep = oAData.Steps.FirstOrDefault(p => p.Id == context.Step.ExternalId);
                oAStep.Status = 100;             
            }
        }
    }
}
