using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpartanChallenge.Models;
using System.Web;

namespace SpartanChallenge.Controllers
{
    public class EquipmentController : ApiController
    {
        public IList<SerialisedEquipment> serialisedEquipmentList = 
            new List<SerialisedEquipment>();

        public IList<EquipmentType> equipmentTypeList =
            new List<EquipmentType>();

        public IList<ListItem> ListItemCollection = 
            new List<ListItem>();

        // GET: api/Equipment/5
        public IHttpActionResult Get()
        {
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/EquipmentData.json");
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string jsonString = reader.ReadToEnd();
                    JObject parsedString = JObject.Parse(jsonString);

                    IList<JToken> serializedResults = parsedString["SerialisedEquipment"].Children().ToList();
                    foreach (JToken item in serializedResults)
                    {
                        SerialisedEquipment equipment = item.ToObject<SerialisedEquipment>();
                        serialisedEquipmentList.Add(equipment);
                    }

                    IList<JToken> typeResults = parsedString["EquipmentType"].Children().ToList();
                    foreach(JToken item in typeResults)
                    {
                        EquipmentType equipmentType = item.ToObject<EquipmentType>();
                        equipmentTypeList.Add(equipmentType);
                    }


                    foreach(SerialisedEquipment equipment in serialisedEquipmentList)
                    {
                        foreach(EquipmentType type in equipmentTypeList)
                        {
                            if (equipment.EquipmentTypeId == type.Id)
                            {
                                ListItem item = new ListItem();
                                item.UnitId = equipment.ExternalId;
                                item.ItemId = type.ExternalId;
                                item.Description = type.Description;
                                ListItemCollection.Add(item);
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("File was not read successfully: " + e.Message);
            }
            
            if (ListItemCollection == null)
            {
                return NotFound();
            }
            return Ok(ListItemCollection);
        }

    }
}
