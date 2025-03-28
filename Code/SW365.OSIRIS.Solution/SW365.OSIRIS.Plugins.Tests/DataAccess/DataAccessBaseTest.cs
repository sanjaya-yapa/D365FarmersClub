﻿using System;
using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.TableModel;

namespace SW365.OSIRIS.Plugins.Tests.DataAccess
{
    public class DataAccessBaseTest<TEntity> where TEntity : Entity
    {
        protected IOrganizationService _organizationService;
        protected CrmServiceContext _serviceContext;

        public DataAccessBaseTest(IOrganizationService organizationService)
        {
            this._organizationService = organizationService;
            this._serviceContext = new CrmServiceContext(organizationService);
        }

        public virtual TEntity GetEntity(Guid id)
        {
            return _organizationService.Retrieve(typeof(TEntity).Name, id, new Microsoft.Xrm.Sdk.Query.ColumnSet(true)).ToEntity<TEntity>();
        }

        public virtual void CreateEntity(TEntity entity)
        {
            _organizationService.Create(entity);
        }   

        public virtual Guid CreateEntityReturnId(TEntity entity)
        {
            return _organizationService.Create(entity);
        }

        public virtual void UpdateEntity(TEntity entity)
        {
            if(!_serviceContext.IsAttached(entity))
            {
                _serviceContext.ClearChanges();
                _serviceContext.Attach(entity);
            }
            _serviceContext.UpdateObject(entity);
            _serviceContext.SaveChanges();
        }

        public virtual void DeleteEntity(Guid id)
        {
            _organizationService.Delete(typeof(TEntity).Name, id);
        }
    }
}
