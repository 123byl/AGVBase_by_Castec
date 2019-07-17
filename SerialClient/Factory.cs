//using AGVDefine;
//using Geometry;
//using SerialClient.Implementation;
//using SerialClient.Interface;
//using SerialCommunicationData;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SerialClient
//{

//    public static class ResponseFactoryClient {

//        /// <summary>
//        /// 建立GetCar回應物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="enb"></param>
//        /// <returns></returns>
//        public static IResponseGetCar GetCar(this IResponseFactory factory,bool enb) {
//            return new InstructionClient.GetCar.Response(EClientPurpose.GetCar, enb);
//        }

//        public static IResponseGetLaser GetLaser(this IResponseFactory factory,IEnumerable<IPair> laserPoints) {
//            return new InstructionClient.GetLaser.Response(EClientPurpose.GetLaser,laserPoints);
//        }

//        /// <summary>
//        /// 建立SetServo回應物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="servoOn"></param>
//        /// <returns></returns>
//        public static IResponseSetServo SetServo(this IResponseFactory factory,bool servoOn) {
//            return new InstructionClient.SetServo.Response(EClientPurpose.SetServo, servoOn);
//        }

//        /// <summary>
//        /// 建立SetWorkVelocity回應物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="velocity"></param>
//        /// <returns></returns>
//        public static IResponseSetWorkVelocity SetWorkVelocity(this IResponseFactory factory,int velocity) {
//            return new InstructionClient.SetWorkVelocity.Response(EClientPurpose.SetWorkVelo, velocity);
//        }

//        /// <summary>
//        /// 建立SetPOS回應物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="suc"></param>
//        /// <returns></returns>
//        public static IResponseSetPOS SetPOS(this IResponseFactory factory,bool suc) {
//            return new InstructionClient.SetPOS.Response(EClientPurpose.SetPOS, suc);
//        }

//        /// <summary>
//        /// 建立SetMoving回應物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="isMoving"></param>
//        /// <returns></returns>
//        public static IResponseSetMoving SetMoving (this IResponseFactory factory,bool isMoving) {
//            return new InstructionClient.SetMoving.Response(EClientPurpose.SetMoving, isMoving);
//        }

//        /// <summary>
//        /// 建立SetDriveVelo回應物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="vLeft"></param>
//        /// <param name="vRight"></param>
//        /// <returns></returns>
//        public static IResponseSetDriveVelo SetDeriveVelo(this IResponseFactory factory,int vLeft,int vRight) {
//            return new InstructionClient.SetDriveVelo.Response(EClientPurpose.SetDriveVelo, vLeft, vRight);
//        }

//        /// <summary>
//        /// 建立SetMode回應物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="mode"></param>
//        /// <returns></returns>
//        public static IResponseSetMode SetMode(this IResponseFactory factory,EMode mode) {
//            return new InstructionClient.SetMode.Response(EClientPurpose.SetMode, mode);
//        }

//        /// <summary>
//        /// 建立SetOriName回應物件
//        /// </summary>
//        /// <param name="facoty"></param>
//        /// <param name="oriName"></param>
//        /// <returns></returns>
//        public static IResponseSetOriName SetOriName(this IResponseFactory facoty,string oriName) {
//            return new InstructionClient.SetOriName.Response(EClientPurpose.SetOriName, oriName);
//        }

//        /// <summary>
//        /// 建立SetPOSComfirm回應物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="similarity"></param>
//        /// <returns></returns>
//        public static IResponseSetPOSComfirm SetPOSComfirm(this IResponseFactory factory,double similarity) {
//            return new InstructionClient.SetPOSComfirm.Response(EClientPurpose.SetPOSComfirm, similarity);
//        }

//        /// <summary>
//        /// 建立SetMapName回應物件
//        /// </summary>
//        /// <param name="facoty"></param>
//        /// <param name="mapName"></param>
//        /// <returns></returns>
//        public static IResponseSetMapName SetMapName(this IResponseFactory facoty,string mapName) {
//            return new InstructionClient.SetMapName.Response(EClientPurpose.SetMapName, mapName);
//        }

//        /// <summary>
//        /// 建立SetPathPlan回應物件
//        /// </summary>
//        /// <param name="facoty"></param>
//        /// <param name="goalIndex"></param>
//        /// <param name="path"></param>
//        /// <returns></returns>
//        public static IResponseSetPathPlan SetPathPlan(this IResponseFactory facoty,bool suc, int goalIndex,List<IPair> path) {
//            return new InstructionClient.SetPathPlan.Response(EClientPurpose.SetPathPaln,suc, goalIndex,path);
//        }

//        /// <summary>
//        /// 建立SetRun回應物件
//        /// </summary>
//        /// <param name="facotry"></param>
//        /// <param name="suc"></param>
//        /// <param name="goalIndex"></param>
//        /// <param name="path"></param>
//        /// <returns></returns>
//        public static IResponseSetRun SetRun(this IResponseFactory facotry,bool suc,int goalIndex,List<IPair> path) {
//            return new InstructionClient.SetRun.Response(EClientPurpose.SetRun, suc, goalIndex, path);
//        }

//        /// <summary>
//        /// 建立SetCharging回應物件
//        /// </summary>
//        /// <param name="facotry"></param>
//        /// <param name="suc"></param>
//        /// <param name="powerIndex"></param>
//        /// <param name="path"></param>
//        /// <returns></returns>
//        public static IResponseSetCharging SetCharging(this IResponseFactory facotry,bool suc,int powerIndex,List<IPair> path) {
//            return new InstructionClient.SetCharging.Response(EClientPurpose.SetCharging, suc, powerIndex, path);
//        }
//    }

//    public static class CommandFactoryClient {

//        /// <summary>
//        /// 建立GetCar命令物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="enb"></param>
//        /// <returns></returns>
//        public static ICommandGetCar GetCar(this ICommandFactory factory,bool enb) {
//            return new InstructionClient.GetCar.Command(EClientPurpose.GetCar, enb);
//        }

//        /// <summary>
//        /// 建立GetLaser命令物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <returns></returns>
//        public static ICommandGetLaser GetLaser(this ICommandFactory factory) {
//            return new InstructionClient.GetLaser.Command(EClientPurpose.GetLaser);
//        }

//        /// <summary>
//        /// 建立SetServo命令物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="servoOn"></param>
//        /// <returns></returns>
//        public static ICommandSetServo SetServo(this ICommandFactory factory,bool servoOn) {
//            return new InstructionClient.SetServo.Command(EClientPurpose.SetServo,servoOn);
//        }

//        /// <summary>
//        /// 建立SetWorkVelocity命令物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="velocity"></param>
//        /// <returns></returns>
//        public static ICommandSetWorkVelocity SetWorkVelocity(this ICommandFactory factory,int velocity) {
//            return new InstructionClient.SetWorkVelocity.Command(EClientPurpose.SetWorkVelo,velocity);
//        }

//        /// <summary>
//        /// 建立SetPOS命令物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="pos"></param>
//        /// <returns></returns>
//        public static ICommandSetPOS SetPOS(this ICommandFactory factory,ITowardPair pos) {
//            return new InstructionClient.SetPOS.Command(EClientPurpose.SetPOS, pos);
//        }

//        /// <summary>
//        /// 建立SetMoving命令物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="start"></param>
//        /// <returns></returns>
//        public static ICommandSetMoving SetMoving(this ICommandFactory factory,bool start) {
//            return new InstructionClient.SetMoving.Command(EClientPurpose.SetMoving, start);
//        }

//        /// <summary>
//        /// 建立SetDriveVelo命令物件
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="vLeft"></param>
//        /// <param name="vRight"></param>
//        /// <returns></returns>
//        public static ICommandSetDriveVelo SetDriveVelo(this ICommandFactory factory,int vLeft,int vRight) {
//            return new InstructionClient.SetDriveVelo.Command(EClientPurpose.SetDriveVelo, vLeft, vRight);
//        }
        
//        /// <summary>
//        /// 建立SetMode命令
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="mode"></param>
//        /// <returns></returns>
//        public static ICommandSetMode SetMode(this ICommandFactory factory,EMode mode) {
//            return new InstructionClient.SetMode.Command(EClientPurpose.SetMode, mode);
//        }

//        /// <summary>
//        /// 建立SetOriName命令
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="oriName"></param>
//        /// <returns></returns>
//        public static ICommandSetOriName SetOriName(this ICommandFactory factory,string oriName) {
//            return new InstructionClient.SetOriName.Command(EClientPurpose.SetOriName,oriName);
//        }

//        /// <summary>
//        /// 建立SetPOSComfirm命令
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <returns></returns>
//        public static ICommandSetPOSComfirm SetPOSComfirm(this ICommandFactory factory) {
//            return new InstructionClient.SetPOSComfirm.Command(EClientPurpose.SetPOSComfirm);
//        }

//        /// <summary>
//        /// 建立SetMapName命令
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="mapName"></param>
//        /// <returns></returns>
//        public static ICommandSetMapName SetMapName(this ICommandFactory factory,string mapName) {
//            return new InstructionClient.SetMapName.Command(EClientPurpose.SetMapName,mapName);
//        }

//        /// <summary>
//        /// 建立SetPathPlan命令
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="goalIndex"></param>
//        /// <returns></returns>
//        public static ICommandSetPathPlan SetPathPlan(this ICommandFactory factory,int goalIndex) {
//            return new InstructionClient.SetPathPlan.Command(EClientPurpose.SetPathPaln, goalIndex);
//        }

//        /// <summary>
//        /// 建立SetRun命令
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="goalIndex"></param>
//        /// <returns></returns>
//        public static ICommandSetRun SetRun(this ICommandFactory factory,int goalIndex) {
//            return new InstructionClient.SetRun.Command(EClientPurpose.SetRun, goalIndex);
//        }

//        /// <summary>
//        /// 建立SetCharging命令
//        /// </summary>
//        /// <param name=""></param>
//        /// <param name="powerIndex"></param>
//        /// <returns></returns>
//        public static ICommandSetCharging SetCharging(this ICommandFactory factory,int powerIndex) {
//            return new InstructionClient.SetCharging.Command(EClientPurpose.SetCharging, powerIndex);
//        }
//    }

//}
