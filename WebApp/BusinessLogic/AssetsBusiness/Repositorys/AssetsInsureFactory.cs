﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCommon.Basic;

namespace BusinessLogic.AssetsBusiness.Repositorys
{
    public class AssetsInsureFactory : IApproveMasterFactory
    {
        public ILoadList CreateListRepository()
        {
            return new AssetsInsureRepository();
        }

        public IApproveEntry CreateApproveEntryRepository()
        {
            return new AssetsInsureRepository();
        }
    }
}
