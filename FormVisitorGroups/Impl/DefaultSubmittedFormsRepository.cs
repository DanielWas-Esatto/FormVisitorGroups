using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EPiServer.Framework;
using EPiServer.ServiceLocation;

namespace FormVisitorGroups.Impl
{
    [ServiceConfiguration(typeof(ISubmittedFormsRepository))]
    public class DefaultSubmittedFormsRepository : ISubmittedFormsRepository
    {
        private readonly string SessionKey = "epiForms:FormSubmissions";

        public bool IsActive()
        {
            if (HttpContext.Current != null &&
                HttpContext.Current.Session != null)
            {
                return true;
            }
            return false;
        }

        public IList<string> GetFormSubmissionIdsList()
        {
            var httpContext = HttpContext.Current;
            Validator.ThrowIfNull("httpContext", httpContext);

            if (httpContext.Session[SessionKey] == null)
            {
                httpContext.Session[SessionKey] = new List<string>();
            }
            return (httpContext.Session[SessionKey] as List<string>);
        }

        public void AddFormSubmissionId(string submissionId)
        {
            if (submissionId == null)
                throw new ArgumentNullException(nameof(submissionId));

            if (IsActive())
                GetFormSubmissionIdsList().Add(submissionId);
        }
    }
}
