using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Utilities
{
    public class EventReceiverUtility
    {
        #region Private Members

        private ClientContext ClientContext { get; set; }

        #endregion

        #region Constructors

        public EventReceiverUtility(ClientContext clientContext)
        {
            this.ClientContext = clientContext;
        }

        #endregion

        public void Bind(ClientContext clientContext,
                        string listName,
                        string eventRecieverName,
                        string remoteUrl,
                        int sequencyId,
                        EventReceiverType eventType)
        {
            try
            {
                List olist = clientContext.Web.Lists.GetByTitle(listName);
                clientContext.Load(olist, o => o.EventReceivers);
                clientContext.ExecuteQuery();
                bool isReRExsist = false;
                foreach (var receiver in olist.EventReceivers)
                {
                    if (receiver.ReceiverName == eventRecieverName)
                    {
                        isReRExsist = true;
                        break;
                    }
                }
                if (!isReRExsist)
                {
                    EventReceiverDefinitionCreationInformation eventReDefCreation = new EventReceiverDefinitionCreationInformation()
                    {

                        EventType = eventType,
                        ReceiverAssembly = Assembly.GetExecutingAssembly().FullName,
                        ReceiverName = eventRecieverName,
                        ReceiverClass = eventRecieverName,
                        ReceiverUrl = remoteUrl,
                        SequenceNumber = sequencyId
                    };
                    olist.EventReceivers.Add(eventReDefCreation);
                    clientContext.ExecuteQuery();

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void UnBind(ClientContext clientContext,
                string listName,
                string eventRecieverName)
        {
            try
            {
                var list = clientContext.Web.Lists.GetByTitle(listName);
                clientContext.Load(list);
                clientContext.ExecuteQuery();
                EventReceiverDefinitionCollection eventRColl = list.EventReceivers;
                clientContext.Load(eventRColl);
                clientContext.ExecuteQuery();
                List<EventReceiverDefinition> toDelete = new List<EventReceiverDefinition>();
                foreach (EventReceiverDefinition erdef in eventRColl)
                {
                    if (erdef.ReceiverName == eventRecieverName)
                    {
                        toDelete.Add(erdef);
                    }
                }
                //Delete the remote event receiver from the list, when the app gets uninstalled
                foreach (EventReceiverDefinition item in toDelete)
                {
                    item.DeleteObject();
                    clientContext.ExecuteQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
