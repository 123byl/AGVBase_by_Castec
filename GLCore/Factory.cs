using Geometry;
using System.Collections.Generic;
using static FactoryMode;

namespace GLCore
{
    /// <summary>
    /// <para>擴充物件製造工廠</para>
    /// </summary>
    public static class Factory
    {
        #region 具執行緒安全 List 物件製造工廠

        /// <summary>
        /// 建立執行緒安全的陣列物件
        /// </summary>
        public static ISafetyList<T> SafetyList<T>(this IFactory factory)
        {
            return new SafetyList<T>();
        }

        /// <summary>
        /// 建立執行緒安全的陣列物件
        /// </summary>
        public static ISafetyList<T> SafetyList<T>(this IFactory factory, IEnumerable<T> collection)
        {
            return new SafetyList<T>(collection);
        }

        /// <summary>
        /// 建立執行緒安全的陣列物件
        /// </summary>
        public static ISafetyList<T> SafetyList<T>(this IFactory factory, T item)
        {
            return new SafetyList<T>(item);
        }

        #endregion 具執行緒安全 List 物件製造工廠

        #region 滑鼠物件製造工廠

        /// <summary>
        /// 建立滑鼠新增面
        /// </summary>
        public static IMouseAddArea MouseAddArea(this IFactory factory, ISingle<IArea> obj)
        {
            return new MouseAddArea(obj);
        }

        /// <summary>
        /// 建立滑鼠新增線
        /// </summary>
        public static IMouseAddLine MouseAddLine(this IFactory factory, ISingle<ILine> obj)
        {
            return new MouseAddLine(obj);
        }

        /// <summary>
        /// 建立滑鼠新增物件
        /// </summary>
        public static IMouseAddTowerPair MouseAddTowerPair(this IFactory factory, ISingle<ITowardPair> obj)
        {
            return new MouseAddTowerPair(obj);
        }

        /// <summary>
        /// 建立滑鼠拖曳物件
        /// </summary>
        public static IMouseDrag MouseDrag(this IFactory factory, DelDragAreaEvent DragAreaEvent, DelDragLineEvent DragLineEvent, DelDragTowardPairEvent DragTowerPairEvent, DelClickAreaEvent ClickAreaEvent, DelClickLineEvent ClickLineEvent, DelClickTowardPairEvent ClickTowardPairEvent)
        {
            return new MouseDrag(true, DragAreaEvent, DragLineEvent, DragTowerPairEvent, ClickAreaEvent, ClickLineEvent, ClickTowardPairEvent);
        }

        /// <summary>
        /// 建立滑鼠擦子物件
        /// </summary>
        public static IMouseEraser MouseEraser(this IFactory factory, int size)
        {
            return new MouseEraser(size);
        }

        /// <summary>
        /// 建立滑鼠插入地圖物件
        /// </summary>
        public static IMouseInsert MouseInsert(this IFactory factory, string filename, IMouseInsertPanel panel)
        {
            return new MouseInsert(filename, panel);
        }

        /// <summary>
        /// 建立滑鼠畫筆物件
        /// </summary>
        public static IMousePen MousePen(this IFactory factory, IPair UITranslate)
        {
            return new MousePen(UITranslate);
        }

        /// <summary>
        /// 建立滑鼠選擇物件
        /// </summary>
        public static IMouseDrag MouseSelect(this IFactory factory, DelDragAreaEvent DragAreaEvent, DelDragLineEvent DragLineEvent, DelDragTowardPairEvent DragTowerPairEvent, DelClickAreaEvent ClickAreaEvent, DelClickLineEvent ClickLineEvent, DelClickTowardPairEvent ClickTowardPairEvent)
        {
            return new MouseDrag(false, DragAreaEvent, DragLineEvent, DragTowerPairEvent, ClickAreaEvent, ClickLineEvent, ClickTowardPairEvent);
        }

        #endregion 滑鼠物件製造工廠

        /// <summary>
        /// 建立色彩物件
        /// </summary>
        public static IColor Color(this IFactory factory, byte r, byte g, byte b, byte a = 255)
        {
            return new Color(r, g, b, a);
        }

        /// <summary>
        /// 建立色彩物件
        /// </summary>
        public static IColor Color(this IFactory factory)
        {
            return new Color();
        }

        /// <summary>
        /// 建立色彩物件
        /// </summary>
        public static IColor Color(this IFactory factory, IColor color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// 建立色彩物件
        /// </summary>
        public static IColor Color(this IFactory factory, System.Drawing.Color color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// 建立色彩物件
        /// </summary>
        public static IColor Color(this IFactory factory, System.Drawing.Color color, byte a)
        {
            return new Color(color.R, color.G, color.B, a);
        }

        /// <summary>
        /// 建立拖曳控制點管理器
        /// </summary>
        public static IDragManager DragManager(this IFactory factory)
        {
            return new DragManager();
        }

        #region Path

        /// <summary>
        /// 建立路徑物件
        /// </summary>
        public static IPath Path(this IFactory factory)
        {
            return new Path();
        }

        /// <summary>
        /// 建立路徑物件
        /// </summary>
        public static IPath Path(this IFactory factory, IEnumerable<IPair> collection)
        {
            return new Path(collection);
        }

        /// <summary>
        /// 建立路徑物件
        /// </summary>
        public static IPath Path(this IFactory factory, IPair item)
        {
            return new Path(item);
        }

        #endregion Path

        #region LaserPoints

        /// <summary>
        /// 建立雷射點物件
        /// </summary>
        public static ILaserPoints LaserPoints(this IFactory factory)
        {
            return new LaserPoints();
        }

        /// <summary>
        /// 建立雷射點物件
        /// </summary>
        public static ILaserPoints LaserPoints(this IFactory factory, IEnumerable<IPair> collection)
        {
            return new LaserPoints(collection);
        }

        /// <summary>
        /// 建立雷射點物件
        /// </summary>
        public static ILaserPoints LaserPoints(this IFactory factory, IPair item)
        {
            return new LaserPoints(item);
        }

        #endregion LaserPoints

        #region ObstaclePoints

        /// <summary>
        /// 建立障礙點物件
        /// </summary>
        public static IObstaclePoints ObstaclePoints(this IFactory factory)
        {
            return new ObstaclePoints();
        }

        /// <summary>
        /// 建立障礙點物件
        /// </summary>
        public static IObstaclePoints ObstaclePoints(this IFactory factory, IEnumerable<IPair> collection)
        {
            return new ObstaclePoints(collection);
        }

        /// <summary>
        /// 建立障礙點物件
        /// </summary>
        public static IObstaclePoints ObstaclePoints(this IFactory factory, IPair item)
        {
            return new ObstaclePoints(item);
        }

        #endregion ObstaclePoints

        #region DynamicObstaclePoints

        /// <summary>
        /// 建立動態障礙點物件
        /// </summary>
        public static IDynamicObstaclePoints DynamicObstaclePoints(this IFactory factory)
        {
            return new DynamicObstaclePoints();
        }

        /// <summary>
        /// 建立動態障礙點物件
        /// </summary>
        public static IDynamicObstaclePoints DynamicObstaclePoints(this IFactory factory, IEnumerable<IPair> collection)
        {
            return new DynamicObstaclePoints(collection);
        }

        /// <summary>
        /// 建立動態障礙點物件
        /// </summary>
        public static IDynamicObstaclePoints DynamicObstaclePoints(this IFactory factory, IPair item)
        {
            return new DynamicObstaclePoints(item);
        }

        #endregion DynamicObstaclePoints

        #region LaserStrength

        /// <summary>
        /// 建立雷射強度點物件
        /// </summary>
        public static ILaserStrength LaserStrength(this IFactory factory)
        {
            return new LaserStrength();
        }

        /// <summary>
        /// 建立雷射強度點物件
        /// </summary>
        public static ILaserStrength LaserStrength(this IFactory factory, IEnumerable<IPair> collection)
        {
            return new LaserStrength(collection);
        }

        /// <summary>
        /// 建立雷射強度點物件
        /// </summary>
        public static ILaserStrength LaserStrength(this IFactory factory, IPair item)
        {
            return new LaserStrength(item);
        }

        #endregion LaserStrength

        #region ObstacleLines

        /// <summary>
        /// 建立障礙線物件
        /// </summary>
        public static IObstacleLines ObstacleLines(this IFactory factory)
        {
            return new ObstacleLines();
        }

        /// <summary>
        /// 建立障礙線物件
        /// </summary>
        public static IObstacleLines ObstacleLines(this IFactory factory, IEnumerable<ILine> collection)
        {
            return new ObstacleLines(collection);
        }

        /// <summary>
        /// 建立障礙線物件
        /// </summary>
        public static IObstacleLines ObstacleLines(this IFactory factory, ILine item)
        {
            return new ObstacleLines(item);
        }

        #endregion ObstacleLines

        #region ForbiddenArea
        /// <summary>
        /// 建立禁止面物件
        /// </summary>
        public static IForbiddenArea ForbiddenArea(this IFactory factory, string name)
        {
            return new ForbiddenArea(name);
        }

        /// <summary>
        /// 建立禁止面物件
        /// </summary>
        public static IForbiddenArea ForbiddenArea(this IFactory factory, int x0, int y0, int x1, int y1, string name)
        {
            return new ForbiddenArea(x0, y0, x1, y1, name);
        }

        /// <summary>
        /// 建立禁止面物件
        /// </summary>
        public static IForbiddenArea ForbiddenArea(this IFactory factory, IArea area, string name)
        {
            return new ForbiddenArea(area, name);
        }

        /// <summary>
        /// 建立禁止面物件
        /// </summary>
        public static IForbiddenArea ForbiddenArea(this IFactory factory, IPair min, IPair max, string name)
        {
            return new ForbiddenArea(min, max, name);
        }
        #endregion ForbiddenArea

        #region AdvancedArea
        /// <summary>
        /// 建立優先面物件
        /// </summary>
        public static IAdvancedArea AdvancedArea(this IFactory factory, string name)
        {
            return new AdvancedArea(name);
        }

        /// <summary>
        /// 建立優先面物件
        /// </summary>
        public static IAdvancedArea AdvancedArea(this IFactory factory, int x0, int y0, int x1, int y1, string name)
        {
            return new AdvancedArea(x0, y0, x1, y1, name);
        }

        /// <summary>
        /// 建立優先面物件
        /// </summary>
        public static IAdvancedArea AdvancedArea(this IFactory factory, IArea area, string name)
        {
            return new AdvancedArea(area, name);
        }

        /// <summary>
        /// 建立優先面物件
        /// </summary>
        public static IAdvancedArea AdvancedArea(this IFactory factory, IPair min, IPair max, string name)
        {
            return new AdvancedArea(min, max, name);
        }
        #endregion AdvancedArea

        #region Eraser

        /// <summary>
        /// 建立擦子
        /// </summary>
        public static IEraser Eraser(this IFactory factory, string name)
        {
            return new Eraser(name);
        }

        /// <summary>
        /// 建立擦子
        /// </summary>
        public static IEraser Eraser(this IFactory factory, int x0, int y0, int x1, int y1, string name)
        {
            return new Eraser(x0, y0, x1, y1, name);
        }

        /// <summary>
        /// 建立擦子
        /// </summary>
        public static IEraser Eraser(this IFactory factory, IArea area, string name)
        {
            return new Eraser(area, name);
        }

        /// <summary>
        /// 建立擦子
        /// </summary>
        public static IEraser Eraser(this IFactory factory, IPair min, IPair max, string name)
        {
            return new Eraser(min, max, name);
        }

        #endregion Eraser

        #region Pen

        /// <summary>
        /// 建立畫筆物件
        /// </summary>
        public static IPen Pen(this IFactory factory, string name)
        {
            return new Pen(name);
        }

        /// <summary>
        /// 建立畫筆物件
        /// </summary>
        public static IPen Pen(this IFactory factory, int x0, int y0, int x1, int y1, string name)
        {
            return new Pen(x0, y0, x1, y1, name);
        }

        /// <summary>
        /// 建立畫筆物件
        /// </summary>
        public static IPen Pen(this IFactory factory, ILine line, string name)
        {
            return new Pen(line, name);
        }

        /// <summary>
        /// 建立畫筆物件
        /// </summary>
        public static IPen Pen(this IFactory factory, IPair beg, IPair end, string name)
        {
            return new Pen(beg, end, name);
        }

        #endregion Pen

        #region ForbiddenLine
        /// <summary>
        /// 建立禁止線物件
        /// </summary>
        public static IForbiddenLine ForbiddenLine(this IFactory factory, string name)
        {
            return new ForbiddenLine(name);
        }

        /// <summary>
        /// 建立禁止線物件
        /// </summary>
        public static IForbiddenLine ForbiddenLine(this IFactory factory, int x0, int y0, int x1, int y1, string name)
        {
            return new ForbiddenLine(x0, y0, x1, y1, name);
        }

        /// <summary>
        /// 建立禁止線物件
        /// </summary>
        public static IForbiddenLine ForbiddenLine(this IFactory factory, ILine line, string name)
        {
            return new ForbiddenLine(line, name);
        }

        /// <summary>
        /// 建立禁止線物件
        /// </summary>
        public static IForbiddenLine ForbiddenLine(this IFactory factory, IPair beg, IPair end, string name)
        {
            return new ForbiddenLine(beg, end, name);
        }
        #endregion ForbiddenLine

        #region AdvancedLine
        /// <summary>
        /// 建立優先線物件
        /// </summary>
        public static IAdvancedLine AdvancedLine(this IFactory factory, string name)
        {
            return new AdvancedLine(name);
        }

        /// <summary>
        /// 建立優先線物件
        /// </summary>
        public static IAdvancedLine AdvancedLine(this IFactory factory, int x0, int y0, int x1, int y1, string name)
        {
            return new AdvancedLine(x0, y0, x1, y1, name);
        }

        /// <summary>
        /// 建立優先線物件
        /// </summary>
        public static IAdvancedLine AdvancedLine(this IFactory factory, ILine line, string name)
        {
            return new AdvancedLine(line, name);
        }

        /// <summary>
        /// 建立優先線物件
        /// </summary>
        public static IAdvancedLine AdvancedLine(this IFactory factory, IPair beg, IPair end, string name)
        {
            return new AdvancedLine(beg, end, name);
        }
        #endregion AdvancedLine

        #region NarrowLine

        /// <summary>
        /// 建立窄道線物件
        /// </summary>
        public static INarrowLine NarrowLine(this IFactory factory, string name)
        {
            return new NarrowLine(name);
        }

        /// <summary>
        /// 建立窄道線物件
        /// </summary>
        public static INarrowLine NarrowLine(this IFactory factory, int x0, int y0, int x1, int y1, string name)
        {
            return new NarrowLine(x0, y0, x1, y1, name);
        }

        /// <summary>
        /// 建立窄道線物件
        /// </summary>
        public static INarrowLine NarrowLine(this IFactory factory, ILine line, string name)
        {
            return new NarrowLine(line, name);
        }

        /// <summary>
        /// 建立窄道線物件
        /// </summary>
        public static INarrowLine NarrowLine(this IFactory factory, IPair beg, IPair end, string name)
        {
            return new NarrowLine(beg, end, name);
        }

        #endregion NarrowLine

        #region AGV

        /// <summary>
        /// 建立 AGV 物件
        /// </summary>
        public static IAGV AGV(string name)
        {
            return new AGV(name);
        }

        /// <summary>
        /// 建立 AGV 物件
        /// </summary>
        public static IAGV AGV(this IFactory factory, int x, int y, double toward, string name)
        {
            return new AGV(x, y, toward, name);
        }

        /// <summary>
        /// 建立 AGV 物件
        /// </summary>
        public static IAGV AGV(this IFactory factory, int x, int y, IAngle toward, string name)
        {
            return new AGV(x, y, toward, name);
        }

        /// <summary>
        /// 建立 AGV 物件
        /// </summary>
        public static IAGV AGV(this IFactory factory, ITowardPair towardPair, string name)
        {
            return new AGV(towardPair, name);
        }

        #endregion AGV

        #region Power

        /// <summary>
        /// 建立充電站物件
        /// </summary>
        public static IPower Power(this IFactory factory, string name)
        {
            return new Power(name);
        }

        /// <summary>
        /// 建立充電站物件
        /// </summary>
        public static IPower Power(this IFactory factory, int x, int y, double toward, string name)
        {
            return new Power(x, y, toward, name);
        }

        /// <summary>
        /// 建立充電站物件
        /// </summary>
        public static IPower Power(this IFactory factory, int x, int y, IAngle toward, string name)
        {
            return new Power(x, y, toward, name);
        }

        /// <summary>
        /// 建立充電站物件
        /// </summary>
        public static IPower Power(this IFactory factory, ITowardPair towardPair, string name)
        {
            return new Power(towardPair, name);
        }

        #endregion Power

        #region Goal

        /// <summary>
        /// 建立目標點物件
        /// </summary>
        public static IGoal Goal(this IFactory factory, string name)
        {
            return new Goal(name);
        }

        /// <summary>
        /// 建立目標點物件
        /// </summary>
        public static IGoal Goal(this IFactory factory, int x, int y, double toward, string name)
        {
            return new Goal(x, y, toward, name);
        }

        /// <summary>
        /// 建立目標點物件
        /// </summary>
        public static IGoal Goal(this IFactory factory, int x, int y, IAngle toward, string name)
        {
            return new Goal(x, y, toward, name);
        }

        /// <summary>
        /// 建立目標點物件
        /// </summary>
        public static IGoal Goal(this IFactory factory, ITowardPair towardPair, string name)
        {
            return new Goal(towardPair, name);
        }

        #endregion Goal

        #region Parking

        /// <summary>
        /// 建立暫時停車區物件
        /// </summary>
        public static IParking Parking(this IFactory factory, string name)
        {
            return new Parking(name);
        }

        /// <summary>
        /// 建立暫時停車區物件
        /// </summary>
        public static IParking Parking(this IFactory factory, int x, int y, double toward, string name)
        {
            return new Parking(x, y, toward, name);
        }

        /// <summary>
        /// 建立暫時停車區物件
        /// </summary>
        public static IParking Parking(this IFactory factory, int x, int y, IAngle toward, string name)
        {
            return new Parking(x, y, toward, name);
        }

        /// <summary>
        /// 建立暫時停車區物件
        /// </summary>
        public static IParking Parking(this IFactory factory, ITowardPair towardPair, string name)
        {
            return new Parking(towardPair, name);
        }

        #endregion Parking
    }
}
