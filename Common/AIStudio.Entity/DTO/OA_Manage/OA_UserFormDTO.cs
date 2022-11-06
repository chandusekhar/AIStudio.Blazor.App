﻿using AIStudio.Entity.OA_Manage;
using AIStudio.Util.Common;
using AIStudio.Util.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStudio.Entity.DTO.OA_Manage
{
    [Map(typeof(OA_UserForm))]
    public class OA_UserFormDTO : OA_UserForm
    {
        public string? ApplicantUserAndDepartment { get => ApplicantUser + "-" + ApplicantDepartment; }
        public string? UserNamesAndRoles
        {
            get
            {
                var users = (UserNames ?? "").Replace("^", " ").Trim().Replace(" ", ",");
                var roles = (UserRoleNames ?? "").Replace("^", " ").Trim().Replace(" ", ",");
                return users + (string.IsNullOrEmpty(roles) ? "" : "-" + roles);
            }
        }

        public string? Current
        {
            get { return CurrentNode?.Replace("^", "").Trim().Replace(" ", ","); }
        }


        public string? ExpectedDateString { get => ExpectedDate?.ToString("yyyy-MM-dd"); }

        public string? WorkflowJSON { get; set; }

        public List<OA_UserFormStepDTO>? Comments { get; set; } = new List<OA_UserFormStepDTO>();

        public List<OAStep>? Steps { get; set; } = new List<OAStep>();

        public int CurrentStepIndex { get; set; }
        public string? CurrentStepId { get; set; }
        public string? Avatar { get; set; }
    }

    public class OA_UserFormInputDTO 
    {
        /// <summary>
        /// 审批进行中
        /// </summary>
        public string? userId { get; set; }
        /// <summary>
        /// 等待审批中
        /// </summary>
        public string? applicantUserId { get; set; }
        /// <summary>
        /// 创建的
        /// </summary>
        public string? creatorId { get; set; }
        /// <summary>
        /// 审批过
        /// </summary>
        public string? alreadyUserIds { get; set; }
    }
}