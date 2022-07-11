using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalog.App.Concrete;

namespace VFHCatalog.App.Abstract
{
    public interface IFileService
    {
        void FileJSONReader();
        void FileErrorJSONReader();
        void FileJSONWriter();
        void FileErrorsJSONWriter();
        void FileXMLReader();
        void FileErrorXMLReader();
        void FileXMLWriter(PlantService plantService);
        void FileErrorsXMLWriter();

    }
}
