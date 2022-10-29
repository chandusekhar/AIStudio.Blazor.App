﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStudio.BlazorUI.Core
{
    public class DisposeLayoutComponentBase : LayoutComponentBase, IDisposable
    {
        public virtual void Dispose()
        {
            
        }
    }
}