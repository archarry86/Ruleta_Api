using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Ruleta_Api.Contracts;
using Ruleta_Api.DataAccess;
using Ruleta_Api.Exceptions;
using Ruleta_Api.Model;
using Ruleta_Api.ResponseModel;

namespace Ruleta_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouletteController : ControllerBase, IRouletteContract
    {
        private readonly AbstracRouletteModel RouletteModel;
        private readonly ILogger<RouletteController> _logger;
        private string idrequest = "";
        public RouletteController(ILogger<RouletteController> logger)
        {
            _logger = logger;
            //I am initialising a MemoryRouletteModel instance ,since I did not have time to implement the redis, or the sqlserver RouletteModel
            //They way to do the Horizontal scale is by game room.
            //a grop of players request the same instance.
            //well the instance shoul not be an  ephemeral machine
            RouletteModel = new MemoryRouletteModel();
            idrequest =  Request.HttpContext.Connection.RemoteIpAddress+"-" +Thread.CurrentThread.ManagedThreadId * 10 + "-[" +DateTime.Now.ToString("yyyyMMdd HH:mm:ss")+"] ";
        }      
        [HttpPost]
        public ApiWrapperResponse<string> CreateBet(CreateBetRequest CreateBetRequest) {
            LogInfo("Start CreateBet");
            ApiWrapperResponse<string> result = new ApiWrapperResponse<string>("", 1);
            try {
                StringValues player_id = "";
                //this header must be encrypted
                HttpContext.Request.Headers.TryGetValue("PlayerIdentification", out player_id);
                if(StringValues.IsNullOrEmpty(player_id)) {
                    result = new ApiWrapperResponse<string>(1,"Invalid Request the player header does not exits.");
                }
                else{
                    CreateBetRequest.bet.AssingPlayerId( player_id);
                    if(RouletteModel.PlayerHasCredit(CreateBetRequest.bet)) {
                        RouletteModel.AddBet(CreateBetRequest.id_roulette, CreateBetRequest.bet);
                    }
                    else {
                        result = new ApiWrapperResponse<string>("Player does not have credit to make the Bet.", 1);
                    }
                }
            }
            catch(BetBoardStateException ex) {
                result = new ApiWrapperResponse<string>(1, ex.Message);
                LogException("Error CreateBet ", ex);
            }
            catch(Exception ex) {
                result = new ApiWrapperResponse<string>(1);
                LogException("Error CreateBet ", ex);
            }
            LogInfo("End CreateBet " + result.ToString());
            return result;
        }
        [HttpPost]
        public ApiWrapperResponse<string> CreateRoulette() {
            LogInfo("Start CreateRoulette");
            ApiWrapperResponse<string> result = new ApiWrapperResponse<string>("",1); 
            try {
                var new_roulette_serial =   RouletteModel.CreateRoullete();
                result = new  ApiWrapperResponse<string>(new_roulette_serial, 0);
            }catch(Exception ex){
                result = new ApiWrapperResponse<string>(1);
                LogException("Error CreateRoulette ", ex);
            }
            LogInfo("End CreateRoulette "+ result.ToString());
            return result;
        }
        [HttpGet]
        public ApiWrapperResponse<IList<RouletteInfo>> GetRoulettes(CreateBetRequest request) {
            LogInfo("Start GetRoulettes");
            ApiWrapperResponse<IList<RouletteInfo>> result = new ApiWrapperResponse<IList<RouletteInfo>>( new List<RouletteInfo>(), 1);
            try {

                var list_of_values = new List<RouletteInfo>();
                var rouletes =  RouletteModel.GetRoulettes();
                var betboardsdictionary = RouletteModel.GetRoulettesBoards();

                list_of_values = rouletes.Select(roulette => {
                    RouletteInfo info = new RouletteInfo();
                    info.Roulette = roulette;
                    List<BetBoard> list_result = null;
                    betboardsdictionary.TryGetValue(roulette.id.ToString(), out list_result);
                    info.BetBoardList = list_result;
                    return info;
                }).ToList();
                result = new ApiWrapperResponse<IList<RouletteInfo>>(list_of_values, 1);
            }
            catch(Exception ex) {
                result = new ApiWrapperResponse<IList<RouletteInfo>>(1);
                LogException("Error GetRoulettes ", ex);
            }
            LogInfo("End GetRoulettes " + result.ToString());
            return result;
        }
        [HttpPost]
        public ApiWrapperResponse<bool> OpenRoulette(string serial ) {
            LogInfo("Start OpenRoulette");
            ApiWrapperResponse<bool> result = new ApiWrapperResponse<bool>(false, 1);
            try {
                RouletteModel.OpenRoulette(serial);
            }
            catch(InvalidOperationException ex) {
                result = new ApiWrapperResponse<bool>(1, ex.Message);
            }
            catch(Exception ex) {
                result = new ApiWrapperResponse<bool>(1);
                LogException("Error OpenRoulette ", ex);
            }
            LogInfo("End OpenRoulette " + result.ToString());
            return result;
        }
        [HttpPost]
        public ApiWrapperResponse<Bet> CloseTheRoulette(string serial) {
            LogInfo("Start CloseTheRoulette");
            ApiWrapperResponse<Bet> result = new ApiWrapperResponse<Bet>(null, 1);
            try {
              var bet_winner=   RouletteModel.CloseRoulette(serial);
                result = new ApiWrapperResponse<Bet>(bet_winner, bet_winner != null? 0 : 1);
            }
            catch(Exception ex) {
                result = new ApiWrapperResponse<Bet>(1);
                LogException("Error CloseTheRoulette ", ex);
            }
            LogInfo("End CloseTheRoulette " + result.ToString());
            return result;
        }
        private void LogInfo(string message) {
            try {
                if(_logger != null)
                    _logger.LogInformation(idrequest + message);
                else
                    Debug.Print(idrequest + message);
            }
            finally { 
            
            }
        }
        private void LogException(string message ,Exception ex) {
            LogInfo(message+" " + ex.Message + " " + ex.GetType().FullName + " " + ex.StackTrace);
        }
    }
}
