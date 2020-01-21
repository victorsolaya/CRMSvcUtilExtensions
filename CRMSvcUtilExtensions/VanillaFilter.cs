﻿using System;
using System.DirectoryServices.ActiveDirectory;
using Microsoft.Crm.Services.Utility;
using Microsoft.Xrm.Sdk.Metadata;
using AttributeMetadata = Microsoft.Xrm.Sdk.Metadata.AttributeMetadata;

namespace CRMSvcUtilExtensions
{
    public class VanillaFilter : Microsoft.Crm.Services.Utility.ICodeWriterFilterService
    {
        private readonly Microsoft.Crm.Services.Utility.ICodeWriterFilterService defaultService;

        public VanillaFilter(Microsoft.Crm.Services.Utility.ICodeWriterFilterService defaultService)
        {
            this.defaultService = defaultService;
        }

        public bool GenerateOptionSet(OptionSetMetadataBase optionSetMetadata, IServiceProvider services)
        {
            return defaultService.GenerateOptionSet(optionSetMetadata, services);
        }

        public bool GenerateOption(OptionMetadata optionMetadata, IServiceProvider services)
        {
            return defaultService.GenerateOption(optionMetadata, services);
        }

        public bool GenerateEntity(EntityMetadata entityMetadata, IServiceProvider services)
        {
            if (entityMetadata.IsCustomEntity.HasValue && entityMetadata.IsCustomEntity.Value)
                return false;

            return defaultService.GenerateEntity(entityMetadata, services);
        }

        public bool GenerateAttribute(AttributeMetadata attributeMetadata, IServiceProvider services)
        {
            if (attributeMetadata.IsCustomAttribute.HasValue && attributeMetadata.IsCustomAttribute.Value)
                return false;

            return defaultService.GenerateAttribute(attributeMetadata, services);
        }

        public bool GenerateRelationship(RelationshipMetadataBase relationshipMetadata, EntityMetadata otherEntityMetadata,
            IServiceProvider services)
        {
            if (relationshipMetadata.IsCustomRelationship.HasValue && relationshipMetadata.IsCustomRelationship.Value)
                return false;

            return defaultService.GenerateRelationship(relationshipMetadata, otherEntityMetadata, services);
        }

        public bool GenerateServiceContext(IServiceProvider services)
        {
            return defaultService.GenerateServiceContext(services);
        }
    }
}