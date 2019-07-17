using Geometry;
using SerialCommunicationData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;

namespace GLCore
{
    /// <summary>
    /// 共用資料
    /// </summary>
    public static class Database
    {
        #region Declaration - Fields

        /// <summary>
        /// 執行緒鎖
        /// </summary>
        private static readonly object mKey = new object();

        private delegate void DelDecode(string data);

        #endregion Declaration - Fields

        #region Data

        /// <summary>
        /// AGV 集合
        /// </summary>
        public static ISafetyDictionary<IAGV> AGVGM { get; } = new SafetyDictionary<IAGV>();

        /// <summary>
        /// 檢查碼
        /// </summary>
        public static string CheckCode { get; private set; } = string.Empty;

        /// <summary>
        /// 動態障礙點集合
        /// </summary>
        public static IDynamicObstaclePoints DynamicObstaclePointsGM { get; } = new DynamicObstaclePoints();

        /// <summary>
        /// 禁止區集合
        /// </summary>
        public static ISafetyDictionary<IForbiddenArea> ForbiddenAreaGM { get; } = new SafetyDictionary<IForbiddenArea>();

        /// <summary>
        /// 禁止線集合
        /// </summary>
        public static ISafetyDictionary<IForbiddenLine> ForbiddenLineGM { get; } = new SafetyDictionary<IForbiddenLine>();

        /// <summary>
        /// 優先區集合
        /// </summary>
        public static ISafetyDictionary<IAdvancedArea> AdvancedAreaGM { get; } = new SafetyDictionary<IAdvancedArea>();

        /// <summary>
        /// 優先線集合
        /// </summary>
        public static ISafetyDictionary<IAdvancedLine> AdvancedLineGM { get; } = new SafetyDictionary<IAdvancedLine>();

        /// <summary>
        /// 目標點集合
        /// </summary>
        public static ISafetyDictionary<IGoal> GoalGM { get; } = new SafetyDictionary<IGoal>();

        /// <summary>
        /// 窄道集合
        /// </summary>
        public static ISafetyDictionary<INarrowLine> NarrowLineGM { get; } = new SafetyDictionary<INarrowLine>();

        /// <summary>
        /// 障礙線集合
        /// </summary>
        public static IObstacleLines ObstacleLinesGM { get; } = new ObstacleLines();

        /// <summary>
        /// 障礙點集合
        /// </summary>
        public static IObstaclePoints ObstaclePointsGM { get; } = new ObstaclePoints();

        /// <summary>
        /// 窄道暫時停車區集合
        /// </summary>
        public static ISafetyDictionary<IParking> ParkingGM { get; } = new SafetyDictionary<IParking>();

        /// <summary>
        /// 充電站集合
        /// </summary>
        public static ISafetyDictionary<IPower> PowerGM { get; } = new SafetyDictionary<IPower>();
        /// <summary>
        /// ID 管理器
        /// </summary>
        public static class ID {
            /// <summary>
            /// ID 累加紀錄
            /// </summary>
            private static uint mID = 0;

            /// <summary>
            /// 產生唯一不重複的 ID
            /// </summary>
            public static uint GenerateID() {
                lock (mKey) {
                    mID++;
                    return mID;
                }
            }
        }

        /// <summary>
        /// 遠端 AGV 管理器
        /// </summary>
        public static class RemoteAGV {
            private static Dictionary<string, uint> mData = new Dictionary<string, uint>();

            /// <summary>
            /// AGV 連線至 EM 時呼叫，此函式負責將 AGV 加入資料庫中
            /// </summary>
            public static void AGVConnected(string IP) {
                lock (mKey) {
                    if (IsExist(IP)) {
                        return;
                    }
                    uint agvID = Database.ID.GenerateID();
                    IAGV agv = new AGV(string.Empty);
                    agv.IP = IP;
                    mData.Add(IP, agvID);
                    Database.AGVGM.Add(agvID, agv);
                }
            }

            /// <summary>
            /// 將 AGV 從資料庫中移除
            /// </summary>
            public static void AGVDisconnected(string IP) {
                lock (mKey) {
                    if (!IsExist(IP)) return;
                    uint agvID = mData[IP];
                    Database.AGVGM.Remove(agvID);
                }
            }

            /// <summary>
            /// 清除雷射 A
            /// </summary>
            public static void ClearLaserA(string IP) {
                lock (mKey) {
                    if (!IsExist(IP)) return;
                    uint agvID = mData[IP];
                    Database.AGVGM.SafetyEdit(agvID, (item) => {
                        item.LaserAPoints.DataList.Clear();
                    });
                }
            }

            /// <summary>
            /// 清除雷射 A、B
            /// </summary>
            public static void ClearLaserAB(string IP) {
                lock (mKey) {
                    if (!IsExist(IP)) return;
                    uint agvID = mData[IP];
                    Database.AGVGM.SafetyEdit(agvID, (item) => {
                        item.LaserAPoints.DataList.Clear();
                        item.LaserBPoints.DataList.Clear();
                    });
                }
            }

            /// <summary>
            /// 清除雷射 B
            /// </summary>
            public static void ClearLaserB(string IP) {
                lock (mKey) {
                    if (!IsExist(IP)) return;
                    uint agvID = mData[IP];
                    Database.AGVGM.SafetyEdit(agvID, (item) => {
                        item.LaserBPoints.DataList.Clear();
                    });
                }
            }

            /// <summary>
            /// 清除路徑
            /// </summary>
            public static void ClearPath(string IP) {
                lock (mKey) {
                    if (!IsExist(IP)) return;
                    uint agvID = mData[IP];
                    Database.AGVGM.SafetyEdit(agvID, (item) => {
                        item.Path.DataList.Clear();
                    });
                }
            }

            /// <summary>
            /// 獲得 AGV 狀態
            /// </summary>
            public static IStatus GetAGVStatus(string IP) {
                lock (mKey) {
                    if (!IsExist(IP)) return null;
                    uint agvID = mData[IP];
                    return Database.AGVGM[agvID].Status;
                }
            }

            /// <summary>
            /// 獲得連線中的所有 IP
            /// </summary>
            public static List<string> GetAllIPs() {
                lock (mKey) {
                    List<string> res = new List<string>();
                    foreach (var item in mData) {
                        res.Add(item.Key);
                    }
                    return res;
                }
            }

            /// <summary>
            /// 更新雷射
            /// </summary>
            public static void UpdateLaserA(string IP, IPair shift, IEnumerable<IPair> laser) {
                lock (mKey) {
                    if (!IsExist(IP)) return;
                    uint agvID = mData[IP];
                    Database.AGVGM.SafetyEdit(agvID, (item) => {
                        item.LaserAPoints.ShowCenter = true;
                        item.LaserAPoints.DataList.Replace(laser);
                        item.LaserAPoints.Center.X = shift.X;
                        item.LaserAPoints.Center.Y = shift.Y;
                    });
                }
            }

            /// <summary>
            /// 更新雷射
            /// </summary>
            public static void UpdateLaserA(string IP, IEnumerable<IPair> laser) {
                lock (mKey) {
                    if (!IsExist(IP)) return;
                    uint agvID = mData[IP];
                    Database.AGVGM.SafetyEdit(agvID, (item) => {
                        item.LaserAPoints.ShowCenter = false;
                        item.LaserAPoints.DataList.Replace(laser);
                    });
                }
            }

            /// <summary>
            /// 更新雷射
            /// </summary>
            public static void UpdateLaserB(string IP, IPair shift, IEnumerable<IPair> laser) {
                lock (mKey) {
                    if (!IsExist(IP)) return;
                    uint agvID = mData[IP];
                    Database.AGVGM.SafetyEdit(agvID, (item) => {
                        item.LaserBPoints.ShowCenter = true;
                        item.LaserBPoints.DataList.Replace(laser);
                        item.LaserBPoints.Center.X = shift.X;
                        item.LaserBPoints.Center.Y = shift.Y;
                    });
                }
            }

            /// <summary>
            /// 更新雷射
            /// </summary>
            public static void UpdateLaserB(string IP, IEnumerable<IPair> laser) {
                lock (mKey) {
                    if (!IsExist(IP)) return;
                    uint agvID = mData[IP];
                    Database.AGVGM.SafetyEdit(agvID, (item) => {
                        item.LaserBPoints.ShowCenter = false;
                        item.LaserBPoints.DataList.Replace(laser);
                    });
                }
            }

            /// <summary>
            /// 更新雷射強度
            /// </summary>
            public static void UpdateLaserStrength(string IP, IEnumerable<IPair> strength) {
                lock (mKey) {
                    if (!IsExist(IP)) return;
                    uint agvID = mData[IP];
                    Database.AGVGM.SafetyEdit(agvID, (item) => {
                        item.LaserStrength.DataList.Replace(strength);
                    });
                }
            }

            /// <summary>
            /// 更新路徑
            /// </summary>
            public static void UpdatePath(string IP, IEnumerable<IPair> path) {
                lock (mKey) {
                    if (!IsExist(IP)) return;
                    uint agvID = mData[IP];
                    Database.AGVGM.SafetyEdit(agvID, (item) => {
                        item.Path.DataList.Replace(path);
                    });
                }
            }

            /// <summary>
            /// 更新 AGV 座標
            /// </summary>
            public static void UpdatePosition(string IP, double x, double y, double toward) {
                lock (mKey) {
                    uint agvID = mData[IP];
                    Database.AGVGM.SafetyEdit(agvID, (item) => {
                        item.Status.Data.Position = FactoryMode.Factory.Pair(x, y);
                        item.Status.Data.Toward.Theta = toward;
                    });
                }
            }

            /// <summary>
            /// 更新AGV座標
            /// </summary>
            /// <param name="IP"></param>
            /// <param name="position"></param>
            public static void UpdatePosition(string IP,ITowardPair position) {
                UpdatePosition(IP, position.Position.X,position.Position.Y, position.Toward.Theta);
            }

            /// <summary>
            /// 更新 AGV 狀態
            /// </summary>
            public static void UpdateStatus(string IP, IStatus status) {
                lock (mKey) {
                    if (status == null || !IsExist(IP)) return;
                    uint agvID = mData[IP];
                    Database.AGVGM.SafetyEdit(agvID, (item) => {
                        item.Status = status;
                    });
                }
            }

            /// <summary>
            /// 此 IP 是否在 Database 中有對應的資料
            /// </summary>
            private static bool IsExist(string IP) {
                lock (mKey) {
                    if (!mData.ContainsKey(IP)) return false;
                    if (!Database.AGVGM.ContainsID(mData[IP])) {
                        mData.Remove(IP);
                        return false;
                    }
                    return true;
                }
            }
        }

        #endregion Data

        #region Funciton - Public Methods

        /// <summary>
        /// 加入標示物
        /// </summary>
        public static void AddMarksToImg(Bitmap map, IArea bound)
        {
            if (map.Width == 0 || map.Height == 0 || bound.Width() == 0 || bound.Height() == 0) return;
            using (Graphics g = Graphics.FromImage(map))
            {
                GoalGM.SafetyForLoop((id, item) => AddTowardPairToImg(g, item, map, bound));
                PowerGM.SafetyForLoop((id, item) => AddTowardPairToImg(g, item, map, bound));
            }
        }

        /// <summary>
        /// 刪除除了 AGV 以外的所有資料
        /// </summary>
        public static void ClearAll()
        {
            AGVGM.Clear();
            ClearAllButAGV();
        }

        /// <summary>
        /// 刪除除了 AGV 以外的所有資料，但不刪除複合元件(如障礙點)的索引
        /// </summary>
        public static void ClearAllButAGV()
        {
            AdvancedAreaGM.Clear();
            AdvancedLineGM.Clear();
            ForbiddenAreaGM.Clear();
            ForbiddenLineGM.Clear();
            GoalGM.Clear();
            NarrowLineGM.Clear();
            ObstacleLinesGM.DataList.Clear();
            ObstaclePointsGM.DataList.Clear();
            DynamicObstaclePointsGM.DataList.Clear();
            ParkingGM.Clear();
            PowerGM.Clear();
        }

        /// <summary>
        /// 產生影像檔，<paramref name="scale"/> = 座標換算率， pixel*<paramref name="scale"/> = mm
        /// </summary>
        public static Bitmap GenerateImg(IArea bound, int scale = 20)
        {
            float pointSize = 1.0f;
            float s = 1.0f / scale;
            int w = bound.Width();
            int h = bound.Height();
            Bitmap bmp = new Bitmap(w / scale + 1, h / scale + 1);

            bmp.Tag = bound.ToString();
            using (Graphics g = Graphics.FromImage(bmp))
            {
                using (System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black))
                {
                    g.Clear(System.Drawing.Color.Wheat);
                    ObstaclePointsGM.DataList.SafetyForLoop((item) =>
                    {
                        // 障礙點座標(mm)轉 bmp 座標
                        float x = (item.X - bound.Min.X) * s;
                        float y = (h - (item.Y - bound.Min.Y)) * s;
                        g.FillRectangle(brush, x, y, pointSize, pointSize);
                    });
                }
            }
            return bmp;
        }

        /// <summary>
        /// 獲得地圖邊界
        /// </summary>
        public static IArea GetBound()
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            bool hasData = false;
            ObstaclePointsGM.DataList.SafetyForLoop((item) =>
            {
                minX = Math.Min(item.X, minX);
                minY = Math.Min(item.Y, minY);
                maxX = Math.Max(item.X, maxX);
                maxY = Math.Max(item.Y, maxY);
                hasData = true;
            });
            GoalGM.SafetyForLoop((id, item) =>
            {
                minX = Math.Min(item.Data.Position.X, minX);
                minY = Math.Min(item.Data.Position.Y, minY);
                maxX = Math.Max(item.Data.Position.X, maxX);
                maxY = Math.Max(item.Data.Position.Y, maxY);
                hasData = true;
            });
            PowerGM.SafetyForLoop((id, item) =>
            {
                minX = Math.Min(item.Data.Position.X, minX);
                minY = Math.Min(item.Data.Position.Y, minY);
                maxX = Math.Max(item.Data.Position.X, maxX);
                maxY = Math.Max(item.Data.Position.Y, maxY);
                hasData = true;
            });
            if (!hasData) return null;
            return FactoryMode.Factory.Area(minX, minY, maxX, maxY);
        }

        /// <summary>
        /// [Client]載入地圖原始檔
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="agvID"></param>
        /// <returns></returns>
        public static IArea LoadOriToDatabase(string filePath,uint agvID) {
            IArea area = null;
            try {
                if (File.Exists(filePath) && Database.AGVGM.ContainsID(agvID)) {
                    var data = File.ReadAllLines(filePath);
                    List<IPair> path = new List<IPair>();
                    double min_x = double.MaxValue, min_y = double.MaxValue, max_x = 0, max_y = 0;
                    foreach (string s in data) {
                        ITowardPair carPos = null;
                        List<IPair> laserData =null;
                        ReadScanningInfo(s, out carPos, out laserData);
                        path.Add(carPos.Position);
                        Database.ObstaclePointsGM.DataList.AddRange(laserData);
                        Database.AGVGM[agvID].Data.Position = carPos.Position;
                        Database.AGVGM[agvID].Data.Toward = carPos.Toward;
                        Database.AGVGM[agvID].Path.DataList.Add(carPos.Position);
                        Database.AGVGM[agvID].LaserAPoints.DataList.Replace(laserData);
                        foreach(var point in laserData) {
                            if (point.X > max_x)
                                max_x = point.X;
                            if (point.X < min_x)
                                min_x = point.X;
                            if (point.Y > max_y)
                                max_y = point.Y;
                            if (point.Y < min_y)
                                min_y = point.Y;
                        }

                        //Thread.Sleep(10);
                    }
                    return FactoryMode.Factory.Area(min_x, min_y, max_x, max_y);
                }
            }catch(Exception ex) {
                Console.WriteLine(ex.Message);

            }
            return area;
        }
        
        /// <summary>
        /// <para>載入地圖到資料庫中，並回傳地圖範圍</para>
        /// </summary>
        public static IArea LoadMapToDatabase(string fileName)
        {
            try
            {
                CheckCode = string.Empty;
                IPair min = null;
                IPair max = null;
                DelDecode decode = null;
                string[] lines = File.ReadAllLines(fileName);
                ClearAllButAGV();
                foreach (var item in lines)
                {
                    string line = item;
                    switch (line)
                    {
                        case AGVDefine.MapFile.Head.GoalList:
                            decode = DecodeGoalList;
                            break;

                        case AGVDefine.MapFile.Head.PowerList:
                            decode = DecodePowerList;
                            break;

                        case AGVDefine.MapFile.Head.ObstaclePoints:
                            decode = DecodeObstaclePoints;
                            break;

                        case AGVDefine.MapFile.Head.ForbiddenLine:
                            decode = DecodeForbiddenLine;
                            break;

                        case AGVDefine.MapFile.Head.ForbiddenArea:
                            decode = DecodeForbiddenArea;
                            break;

                        case AGVDefine.MapFile.Head.AdvancedLine:
                            decode = DecodeAdvancedLine;
                            break;

                        case AGVDefine.MapFile.Head.AdvancedArea:
                            decode = DecodeAdvancedArea;
                            break;

                        default:
                            if (line.StartsWith(AGVDefine.MapFile.Head.CheckCode))
                            {
                                CheckCode = DecodeCheckCode(line);
                            }
                            else if (line.StartsWith(AGVDefine.MapFile.Head.MinPosition))
                            {
                                min = DecodeMinPosition(line);
                            }
                            else if (line.StartsWith(AGVDefine.MapFile.Head.MaxPosition))
                            {
                                max = DecodeMaxPosition(line);
                            }
                            else
                            {
                                try
                                {
                                    decode?.Invoke(line);
                                }
                                catch (Exception)
                                {
                                }
                            }
                            break;
                    }
                }
                if (min != null && max != null) return FactoryMode.Factory.Area(min, max);
            }
            catch (System.Exception)
            {
            }
            return null;
        }

        /// <summary>
        /// 儲存資料到文字檔
        /// </summary>
        public static void Save(string fileName)
        {
            List<string> lines = new List<string>();
            IPair min = null;
            IPair max = null;

            lines.Add(AGVDefine.MapFile.Head.GoalList);
            lines.AddRange(GoalGM.ToStringList());
            lines.Add(AGVDefine.MapFile.Head.PowerList);
            lines.AddRange(PowerGM.ToStringList());
            lines.Add(AGVDefine.MapFile.Head.ForbiddenLine);
            lines.AddRange(ForbiddenLineGM.ToStringList());
            lines.Add(AGVDefine.MapFile.Head.ForbiddenArea);
            lines.AddRange(ForbiddenAreaGM.ToStringList());
            lines.Add(AGVDefine.MapFile.Head.AdvancedLine);
            lines.AddRange(AdvancedLineGM.ToStringList());
            lines.Add(AGVDefine.MapFile.Head.AdvancedArea);
            lines.AddRange(AdvancedAreaGM.ToStringList());
            lines.Add(AGVDefine.MapFile.Head.ObstaclePoints);
            lines.AddRange(EncodeObstaclePoints(out min, out max));
            lines.InsertRange(0, EncodeBound(min, max));
            lines.Insert(0, AGVDefine.MapFile.Head.CheckCode + DateTime.Now.Ticks);

            File.WriteAllLines(fileName, lines);
        }

        /// <summary>
        /// 讀取單一筆掃描資訊
        /// </summary>
        /// <param name="pack"></param>
        /// <param name="carPos"></param>
        /// <param name="laserData"></param>
        public static void ReadScanningInfo(string pack, out ITowardPair carPos, out List<IPair> laserData) {
            try {
                string[] info = pack.Split(new char[] { ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
                carPos = FactoryMode.Factory.TowardPair(double.Parse(info[0]), double.Parse(info[1]), double.Parse(info[2]));
                laserData = new List<IPair>();
                for (int m = 3; m < info.Length - 1; m += 2) {
                    laserData.Add(FactoryMode.Factory.Pair(double.Parse(info[m]), double.Parse(info[m + 1])));
                }
                //return true;
            } catch (Exception error) {
                carPos = null;
                laserData = null;
                Console.WriteLine(error);
                //return false;
            }
        }
        
        #endregion Funciton - Public Methods

        #region Function - Private Methdos
        
        /// <summary>
        /// 加入標示物
        /// </summary>
        private static void AddTowardPairToImg<T>(Graphics g, T item, Bitmap map, IArea bound) where T : ISingle<ITowardPair>
        {
            float scale = Math.Min(((float)map.Width) / bound.Width(), ((float)map.Height) / bound.Height());

            float cx = (item.Data.Position.X - bound.Min.X) * scale;
            float cy = (bound.Height() - (item.Data.Position.Y - bound.Min.Y)) * scale;
            float w = item.GLSetting.Size.X * scale;
            float h = item.GLSetting.Size.Y * scale;

            System.Drawing.Color color = item.GLSetting.MainColor.NetColor();
            using (System.Drawing.Brush brush = new System.Drawing.SolidBrush(color))
            {
                g.FillRectangle(brush, cx, cy, w, h);
            }
            using (System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue))
            {
                using (Font font = new Font("Arial", 24))
                {
                    g.DrawString(item.Name, font, brush, cx, cy);
                }
            }
        }
        
        private static string DecodeCheckCode(string line)
        {
            try
            {
                string[] para = line.Split(new char[] { ':' });
                return para[1];
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }

        private static void DecodeForbiddenArea(string data)
        {
            string[] elm = data.Split(AGVDefine.MapFile.SplitChar);
            ForbiddenArea farea = new ForbiddenArea((int)double.Parse(elm[1]), (int)double.Parse(elm[2]), (int)double.Parse(elm[3]), (int)double.Parse(elm[4]), elm[0]);
            ForbiddenAreaGM.Add(ID.GenerateID(), farea);
        }

        private static void DecodeForbiddenLine(string data)
        {
            string[] elm = data.Split(AGVDefine.MapFile.SplitChar);
            ForbiddenLine fline = new ForbiddenLine((int)double.Parse(elm[1]), (int)double.Parse(elm[2]), (int)double.Parse(elm[3]), (int)double.Parse(elm[4]), elm[0]);
            ForbiddenLineGM.Add(ID.GenerateID(), fline);
        }

        private static void DecodeAdvancedArea(string data)
        {
            string[] elm = data.Split(AGVDefine.MapFile.SplitChar);
            AdvancedArea aarea = new AdvancedArea((int)double.Parse(elm[1]), (int)double.Parse(elm[2]), (int)double.Parse(elm[3]), (int)double.Parse(elm[4]), elm[0]);
            AdvancedAreaGM.Add(ID.GenerateID(), aarea);
        }

        private static void DecodeAdvancedLine(string data)
        {
            string[] elm = data.Split(AGVDefine.MapFile.SplitChar);
            AdvancedLine aline = new AdvancedLine((int)double.Parse(elm[1]), (int)double.Parse(elm[2]), (int)double.Parse(elm[3]), (int)double.Parse(elm[4]), elm[0]);
            AdvancedLineGM.Add(ID.GenerateID(), aline);
        }

        private static void DecodeGoalList(string data)
        {
            string[] elm = data.Split(AGVDefine.MapFile.SplitChar);
            Goal goal = new Goal((int)double.Parse(elm[1]), (int)double.Parse(elm[2]), double.Parse(elm[3]), elm[0]);
            GoalGM.Add(ID.GenerateID(), goal);
        }

        private static IPair DecodeMaxPosition(string line)
        {
            try
            {
                double x = 0, y = 0;
                string[] para = line.Split(new char[] { ':', ',' });
                x = double.Parse(para[1]);
                y = double.Parse(para[2]);
                return FactoryMode.Factory.Pair(x, y);
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static IPair DecodeMinPosition(string line)
        {
            try
            {
                double x = 0, y = 0;
                string[] para = line.Split(new char[] { ':', ',' });
                x = double.Parse(para[1]);
                y = double.Parse(para[2]);
                return FactoryMode.Factory.Pair(x, y);
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static void DecodeObstaclePoints(string data)
        {
            string[] elm = data.Split(AGVDefine.MapFile.SplitChar);
            IPair point = FactoryMode.Factory.Pair(double.Parse(elm[0]), double.Parse(elm[1]));
            ObstaclePointsGM.DataList.Add(point);
        }

        private static void DecodePowerList(string data)
        {
            string[] elm = data.Split(AGVDefine.MapFile.SplitChar);
            Power power = new Power((int)double.Parse(elm[1]), (int)double.Parse(elm[2]), double.Parse(elm[3]), elm[0]);
            PowerGM.Add(ID.GenerateID(), power);
        }

        private static IEnumerable<string> EncodeBound(IPair min, IPair max)
        {
            List<string> res = new List<string>();
            res.Add(AGVDefine.MapFile.Head.MinPosition + (min?.ToString() ?? string.Empty));
            res.Add(AGVDefine.MapFile.Head.MaxPosition + (max?.ToString() ?? string.Empty));
            return res;
        }

        private static IEnumerable<string> EncodeObstaclePoints(out IPair min, out IPair max)
        {
            IPair _min = null;
            IPair _max = null;
            List<string> res = new List<string>();
            ObstaclePointsGM.DataList.SafetyForLoop((item) =>
            {
                if (_min == null) _min = FactoryMode.Factory.Pair(item);
                if (_max == null) _max = FactoryMode.Factory.Pair(item);
                _min.X = Math.Min(_min.X, item.X);
                _min.Y = Math.Min(_min.Y, item.Y);
                _max.X = Math.Max(_max.X, item.X);
                _max.Y = Math.Max(_max.Y, item.Y);
                res.Add(item.ToString());
            });
            min = _min;
            max = _max;
            return res;
        }

        #endregion Function - Private Methods
    }
}