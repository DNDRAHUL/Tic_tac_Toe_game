namespace Tictoe.Helper
{
    public static class Util
    {

        public static bool IsNull(this object oObject)
        {
            return oObject == null ? true : false;
        }

        public static bool IsNullOrEmpty(this object[] oObject)
        {
            return (oObject == null || oObject.Length == 0);
        }

        public static bool IsNotNull(this object oObject)
        {
            return oObject == null ? false : true;

        }
    }
}
