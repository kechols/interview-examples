using System;

namespace Sunrise.Radiology.Messenger.Common.Legacy
{
    public class ConvertibleDateTimeOffset : IConvertible
    {
        private DateTimeOffset dateTimeOffset;

        public static ConvertibleDateTimeOffset Parse(string dateTimeOffsetString)
        {
            return new ConvertibleDateTimeOffset(DateTimeOffset.Parse(dateTimeOffsetString));
        }


        public ConvertibleDateTimeOffset(DateTimeOffset dateTimeOffset)
        {
            this.dateTimeOffset = dateTimeOffset;
        }


        public TypeCode GetTypeCode()
        {
            return dateTimeOffset.DateTime.GetTypeCode();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return AsIConvertible().ToBoolean(provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return AsIConvertible().ToChar(provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return AsIConvertible().ToSByte(provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return AsIConvertible().ToByte(provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return AsIConvertible().ToByte(provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return AsIConvertible().ToUInt16(provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return AsIConvertible().ToInt32(provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return AsIConvertible().ToUInt32(provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return AsIConvertible().ToInt64(provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return AsIConvertible().ToUInt64(provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return AsIConvertible().ToSingle(provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return AsIConvertible().ToDouble(provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return AsIConvertible().ToDecimal(provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return dateTimeOffset.DateTime;
        }

        public string ToString(IFormatProvider provider) => ToDateTime(provider).ToString();

        public object ToType(Type conversionType, IFormatProvider provider) => ToDateTime(provider);

        private IConvertible AsIConvertible()
        {
            return ((IConvertible) dateTimeOffset.DateTime);
        }
    }
}
