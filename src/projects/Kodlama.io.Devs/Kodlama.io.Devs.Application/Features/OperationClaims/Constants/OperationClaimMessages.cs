﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.OperationClaims.Constants
{
    public class OperationClaimMessages
    {
        public const string NameIsRequired = "Operation claim name is required";
        public const string IdIsRequired = "Operation claim id is required";
        public const string GreaterThanZero = "Operation claim id must be greater than zero";
        public const string NameCanNotBeDuplicated = "Operation claim name can not be duplicated";
        public const string IdShouldBeExist = "Operation claim id should be exist";
        public const string DoesNotHaveAnyRecords = "Operation claim does not have any records";
    }
}
