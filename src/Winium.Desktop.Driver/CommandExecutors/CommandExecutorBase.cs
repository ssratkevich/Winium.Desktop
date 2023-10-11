using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Winium.Cruciatus.Elements;
using Winium.Desktop.Driver.Automation;
using Winium.StoreApps.Common;
using Winium.StoreApps.Common.Exceptions;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal abstract class CommandExecutorBase
    {
        #region Public Properties

        public Command ExecutedCommand { get; set; }

        #endregion

        #region Properties

        protected Automator Automator { get; set; }

        #endregion

        #region Public Methods and Operators

        public CommandResponse Do()
        {
            if (this.ExecutedCommand == null)
            {
                throw new NullReferenceException("ExecutedCommand property must be set before calling Do");
            }

            try
            {
                var session = this.ExecutedCommand.SessionId;
                this.Automator = Automator.InstanceForSession(session);
                return CommandResponse.Create(HttpStatusCode.OK, this.DoImpl());
            }
            catch (AutomationException exception)
            {
                return CommandResponse.Create(JsonErrorCodes.GetErrorStatusCode(exception.ErrorCode), this.JsonResponse(exception.ErrorCode, exception));
            }
            catch (NotImplementedException exception)
            {
                return CommandResponse.Create(
                    HttpStatusCode.NotImplemented,
                    this.JsonResponse(ErrorCodes.UnknownCommand, exception));
            }
            catch (Exception exception)
            {
                return CommandResponse.Create(
                    HttpStatusCode.InternalServerError,
                    this.JsonResponse(ErrorCodes.UnknownError, exception));
            }
        }

        #endregion

        #region Methods

        protected abstract string DoImpl();

        /// <summary>
        /// The JsonResponse with SUCCESS status and NULL value.
        /// </summary>
        protected string JsonResponse()
        {
            return this.JsonResponse(ErrorCodes.Success, null);
        }

        protected string JsonResponse(ErrorCodes status, object value)
        {
            return JsonConvert.SerializeObject(
                new JsonResponse(this.Automator.Session, status, value),
                Formatting.Indented);
        }

        protected CruciatusElement TryGetElement(JToken value) =>
            value != null
            ? this.TryGetElement(value["element-6066-11e4-a52e-4f735466cecf"].ToString())
            : null;

        protected CruciatusElement TryGetElement(string elementId) =>
            this.Automator.ElementsRegistry.GetRegisteredElement(elementId);

        #endregion
    }
}
