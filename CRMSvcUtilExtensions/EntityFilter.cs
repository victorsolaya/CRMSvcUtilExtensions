using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Crm.Services.Utility;
using Microsoft.Xrm.Sdk.Metadata;

namespace CRMSvcUtilExtensions
{
    public class EntityFilter : ICodeWriterFilterService
    {
        private readonly ICodeWriterFilterService defaultService;
        private const string filterXML = "filter.xml";
        private List<string> _filterEntities = new List<string>();

        public EntityFilter(ICodeWriterFilterService defaultService)
        {
            this.defaultService = defaultService;
            this.LoadFilter();
        }

        private void LoadFilter()
        {
            string filterXml = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\" + filterXML;
            Console.WriteLine(filterXml);
            XmlDocument doc = new XmlDocument();
            doc.Load(filterXml);
            XmlNodeList entities = doc.GetElementsByTagName("entity");
            foreach (XmlNode i in entities)
            {
                _filterEntities.Add(i.InnerText);
            }
        }

        private bool IncludeEntity(string entityname)
        {
            return (_filterEntities.Contains(entityname));
        }
        
        public bool GenerateOptionSet(
            OptionSetMetadataBase optionSetMetadata, IServiceProvider services)
        {
            return defaultService.GenerateOptionSet(optionSetMetadata, services);

        }

        public bool GenerateOption(OptionMetadata optionMetadata, IServiceProvider services)
        {
            return defaultService.GenerateOption(optionMetadata, services);
        }

        public bool GenerateEntity(EntityMetadata entityMetadata, IServiceProvider services)
        {


            if (!this.IncludeEntity(entityMetadata.LogicalName))
            {
                return false;
            }
            return defaultService.GenerateEntity(entityMetadata, services);

        }

        public bool GenerateAttribute(AttributeMetadata attributeMetadata, IServiceProvider services)
        {

            if (!this.IncludeEntity(attributeMetadata.EntityLogicalName))
            {
                return false;
            }
            return defaultService.GenerateAttribute(attributeMetadata, services);
        }

        public bool GenerateRelationship(RelationshipMetadataBase relationshipMetadata, EntityMetadata otherEntityMetadata,
            IServiceProvider services)
        {
            return defaultService.GenerateRelationship(relationshipMetadata, otherEntityMetadata, services);
        }

        public bool GenerateServiceContext(IServiceProvider services)
        {
            return defaultService.GenerateServiceContext(services);
        }
    }
}