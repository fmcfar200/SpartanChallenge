﻿using System;
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
using System.Collections;

namespace SpartanChallenge.Controllers
{
    public class EquipmentController : ApiController
    {
        public List<SerialisedEquipment> serialisedEquipmentList = 
            new List<SerialisedEquipment>();

        public List<EquipmentType> equipmentTypeList =
            new List<EquipmentType>();

        public List<ListItem> ListItemCollection = 
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
                    foreach (JToken item in typeResults)
                    {
                        EquipmentType equipmentType = item.ToObject<EquipmentType>();
                        equipmentTypeList.Add(equipmentType);
                    }


                    foreach (SerialisedEquipment equipment in serialisedEquipmentList)
                    {
                        foreach (EquipmentType type in equipmentTypeList)
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


        

        /*
        
        // GET: api/Equipment/5
        public IHttpActionResult Get(int searchTerm)
        {
            List<ListItem> queryResult = new List<ListItem>();

            try
            {
                queryResult = ListItemCollection.FindAll(s => s.UnitId.Equals(searchTerm));
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not get search: " + e.Message);
            }
            

            if (queryResult == null)
            {
                return NotFound();
            }
            return Ok(searchTerm);
        }

            */


        
        // Post: api/Equipment
        public IEnumerable Post([FromBody]string searchTerm)
        {
            List<ListItem> queryResult = new List<ListItem>();

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
                    foreach (JToken item in typeResults)
                    {
                        EquipmentType equipmentType = item.ToObject<EquipmentType>();
                        equipmentTypeList.Add(equipmentType);
                    }


                    foreach (SerialisedEquipment equipment in serialisedEquipmentList)
                    {
                        foreach (EquipmentType type in equipmentTypeList)
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

            if (searchTerm != null)
            {
                try
                {
                    int query = Int32.Parse(searchTerm);
                    queryResult = ListItemCollection.FindAll(s => s.UnitId.Equals(query));

                    if (queryResult.Count == 0 || queryResult == null)
                    {
                        queryResult = ListItemCollection;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not get search: " + e.Message);
                }
            }
            else if (searchTerm == null)
            {
                queryResult = ListItemCollection;
            }

            return queryResult;

        }
        

    }
}
