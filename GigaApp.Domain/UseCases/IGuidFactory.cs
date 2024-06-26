﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases
{
    public interface IGuidFactory
    {
        Guid Create();
    }

    public class GuidFactory : IGuidFactory
    {
        public Guid Create()=>Guid.NewGuid();
    }
}
