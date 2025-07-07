using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TICRM.BuisnessLayer.Base;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.BuisnessLayer
{
    public class GraphManager : BaseManager
    {
        public List<GraphDto> GetGraphList()
        {
            try
            {
                InsertEventLog("GetGraphList", EventType.Log, EventColor.yellow, "Get List Of Graph","TICRM.BusinessLayer.GraphManager.GetGraphList", "");
                List<GraphDto> graphDtos = new List<GraphDto>();
                List<Graph> graph = dbEnt.Graphs.ToList();
                foreach (Graph item in graph)
                {
                    graphDtos.Add(objMapper.GetGraphDto(item));
                }
                return graphDtos;
            }
            catch (Exception ex)
            {
                InsertEventMonitor("GetGraphList", EventType.Exception, EventColor.red, ex.Message + " /n " + ex.StackTrace, "TICRM.BusinessLayer.GraphManager.GetGraphList", "");
                throw;
            }
        }



    }
}
