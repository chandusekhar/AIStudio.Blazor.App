﻿using AIStudio.Util.Mapper;
using AIStudio.Entity.Base_Manage;
using AIStudio.Util.Common;

namespace AIStudio.Entity.DTO.Base_Manage
{
    [Map(typeof(Base_LogSystem))]
    public class Base_LogSystemDTO : Base_LogSystem, IIdObject
    {

    }
}