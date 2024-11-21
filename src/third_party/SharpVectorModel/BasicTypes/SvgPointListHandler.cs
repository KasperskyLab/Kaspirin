namespace SharpVectors.Dom.Svg
{
    public class SvgPointListHandler : ISvgPointsHandler
    {
        private SvgPointList _pointList;

        public SvgPointListHandler(SvgPointList pointList)
        {
            _pointList = pointList;
        }

        public void StartPoints()
        {
            _pointList.Clear();
        }

        public void EndPoints()
        {
        }

        public void Point(float x, float y)
        {
            _pointList.AppendItem(new SvgPoint(x, y));
        }
    }
}
