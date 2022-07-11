using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalog.App.Common;
using VFHCatalog.App.Abstract;
using VFHCatalog.Domain.Common;
using VFHCatalog.Domain.Entity;
using VFHCatalog.App.Helpers;
using System.IO;

namespace VFHCatalog.App.Concrete
{
    public class ErrorsService : BaseService<Error>
    {
        
        MessagesService message = new MessagesService();

        public ErrorsService GetErrorsFromId(int lastId, ErrorsService errorsService)
        {
            ErrorsService errors = new ErrorsService();
            var listOfErrors = errorsService.GetAllItemsFromID(lastId);
            if (listOfErrors.Count > 0)
                errors.AddItems(listOfErrors);

            return errors;
        }
        public Error SetErrorParameters(Exception e)
        {
            Error error = new Error();
            error.Message = e.Message;
            error.Source = e.Source;
            error.StackTrace = e.StackTrace;
            //error.InnerException = e.InnerException.Message;
            error.DateTime = DateTime.Now;
            return error;
            
        }

        public Error CatchError(Exception e, ErrorsService errorsService)
        {
            Console.WriteLine(message._ERROR_SERVICE);
            Console.WriteLine();
            var error = errorsService.SetErrorParameters(e);

            errorsService.AddItem(error);

            return error;
        }

    }
}
