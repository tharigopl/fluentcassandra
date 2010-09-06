﻿using System;
using System.ComponentModel;

namespace FluentCassandra.Types
{
	public class LongType : CassandraType
	{
		private static readonly LongTypeConverter Converter = new LongTypeConverter();

		#region Implimentation

		public override object GetValue(Type type)
		{
			var converter = Converter;

			if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
			{
				var nc = new NullableConverter(type);
				type = nc.UnderlyingType;
			}

			if (!converter.CanConvertTo(type))
				throw new InvalidCastException(String.Format("{0} cannot be cast to {1}", type, TypeCode));

			return converter.ConvertTo(_value, type);
		}

		public override CassandraType SetValue(object obj)
		{
			var converter = Converter;

			if (!converter.CanConvertFrom(obj.GetType()))
				throw new InvalidCastException(String.Format("{0} cannot be cast to {1}", obj.GetType(), TypeCode));

			_value = (long)converter.ConvertFrom(obj);

			return this;
		}

		protected override TypeCode TypeCode
		{
			get { return TypeCode.Int64; }
		}

		public override byte[] ToByteArray()
		{
			return GetValue<byte[]>();
		}

		public override string ToString()
		{
			return _value.ToString("N");
		}

		#endregion

		private long _value;

		#region Equality

		public override bool Equals(object obj)
		{
			if (obj is LongType)
				return _value == ((LongType)obj)._value;

			return _value == CassandraType.GetValue<long>(obj, Converter);
		}

		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}

		#endregion

		#region Conversion

		public static implicit operator long(LongType type)
		{
			return type._value;
		}

		public static implicit operator long?(LongType type)
		{
			return type._value;
		}

		public static implicit operator LongType(long s)
		{
			return new LongType { _value = s };
		}

		public static implicit operator byte[](LongType o) { return ConvertTo<byte[]>(o); }
		public static implicit operator LongType(byte[] o) { return ConvertFrom(o); }

		public static implicit operator LongType(byte o) { return ConvertFrom(o); }
		public static implicit operator LongType(sbyte o) { return ConvertFrom(o); }
		public static implicit operator LongType(short o) { return ConvertFrom(o); }
		public static implicit operator LongType(ushort o) { return ConvertFrom(o); }
		public static implicit operator LongType(int o) { return ConvertFrom(o); }
		public static implicit operator LongType(uint o) { return ConvertFrom(o); }

		public static implicit operator LongType(ulong o) { return ConvertFrom(o); }

		public static implicit operator byte(LongType o) { return ConvertTo<byte>(o); }
		public static implicit operator sbyte(LongType o) { return ConvertTo<sbyte>(o); }
		public static implicit operator short(LongType o) { return ConvertTo<short>(o); }
		public static implicit operator ushort(LongType o) { return ConvertTo<ushort>(o); }
		public static implicit operator int(LongType o) { return ConvertTo<int>(o); }
		public static implicit operator uint(LongType o) { return ConvertTo<uint>(o); }

		public static implicit operator ulong(LongType o) { return ConvertTo<ulong>(o); }

		public static implicit operator byte?(LongType o) { return ConvertTo<byte>(o); }
		public static implicit operator sbyte?(LongType o) { return ConvertTo<sbyte>(o); }
		public static implicit operator short?(LongType o) { return ConvertTo<short>(o); }
		public static implicit operator ushort?(LongType o) { return ConvertTo<ushort>(o); }
		public static implicit operator int?(LongType o) { return ConvertTo<int>(o); }
		public static implicit operator uint?(LongType o) { return ConvertTo<uint>(o); }

		public static implicit operator ulong?(LongType o) { return ConvertTo<ulong>(o); }

		private static T ConvertTo<T>(LongType type)
		{
			if (type == null)
				return default(T);

			return type.GetValue<T>();
		}

		private static LongType ConvertFrom(object o)
		{
			var type = new LongType();
			type.SetValue(o);
			return type;
		}

		#endregion
	}
}
