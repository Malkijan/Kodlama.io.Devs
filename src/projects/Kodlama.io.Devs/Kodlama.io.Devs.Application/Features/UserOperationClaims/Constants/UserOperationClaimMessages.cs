﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Constants
{
    public class UserOperationClaimMessages
    {
        public const string OperationClaimIdIsRequired = "User Operation claim id is required";
        public const string UserIdIsRequired = "User id is required";
        public const string IdIsRequired = "User Operation claim id is required";
        public const string UserIdGreaterThanZero = "User Id must be greater than zero";
        public const string IdGreaterThanZero = "Id must be greater than zero";
        public const string OperationClaimIdGreaterThanZero = "Operation Claim Id must be greater than zero";
        public const string UserIdAndOperationClaimIdCanNotBeDuplicated = "User id and operation claim id can not be duplicated";
        public const string IdShouldBeExist = "User Operation claim id should be exist";
        public const string DoesNotHaveAnyRecords = "User Operation claim does not have any records";
        public const string UserDoesNotExists = "User does not exists";
        public const string OperationClaimDoesNotExists = "Operation claim does not exists";
    }
}
