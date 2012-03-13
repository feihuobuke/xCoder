// ************************************************************************************************
// *								       
// *	Copyright (c) 2012, xCoder Project Team All rights reserved.	       
// *	@xCoder/xCoder.DB2Project/TypeMap.cs                                                                   
// *	Created @ 03/09/2012 8:19 PM							       
// *	By Hermanxwong@Codeplex					         
// *								         
// *	This Project follows BSD License					        
// ************************************************************************************************

using System;
using System.Linq;
using System.Reflection;
using xCoder.DB2Project.Attributes;

namespace xCoder.DB2Project.Data.Type
{
    public struct TypeMap
    {
        [DBType("BigInt")] public const string BigInt = "double";

        [DBType("Binary")] public const string Binary = "byte[]";

        [DBType("Bit")] public const string Bit = "bool";

        [DBType("Char")] public const string Char = "char";

        [DBType("DateTime")] public const string DateTime = "DateTime";

        [DBType("Decimal")] public const string Decimal = "decimal";

        [DBType("Float")] public const string Float = "float";

        [DBType("Image")] public const string Image = "byte[]";

        [DBType("Int")]
        [DBType("numeric")]
        public const string Int = "int";

        [DBType("Money")] public const string Money = "float";

        [DBType("NChar")] public const string NChar = "stirng";

        [DBType("NText")] public const string NText = "stirng";

        [DBType("NVarChar")] public const string NVarChar = "stirng";

        [DBType("Real")] public const string Real = "float";

        [DBType("UniqueIdentifier")] [DBType("uuid")] public const string UniqueIdentifier = "Guid";

        [DBType("SmallDateTime")] public const string SmallDateTime = "DateTime";

        [DBType("SmallInt")] public const string SmallInt = "Int16";

        [DBType("SmallMoney")] public const string SmallMoney = "decimal";

        [DBType("Text")] public const string Text = "string";

        [DBType("Timestamp")] public const string Timestamp = "object";

        [DBType("TinyInt")] public const string TinyInt = "byte";

        [DBType("VarBinary")] public const string VarBinary = "byte[]";

        [DBType("VarChar")] public const string VarChar = "string";

        [DBType("Variant")] public const string Variant = "object";

        [DBType("Xml")] public const string Xml = "byte[]";

        [DBType("Udt")] public const string Udt = "object";

        [DBType("Structured")] public const string Structured = "object";

        [DBType("Date")] public const string Date = "DateTime";

        [DBType("Time")] public const string Time = "DateTime";

        [DBType("DateTime2")] public const string DateTime2 = "DateTime";

        [DBType("DateTimeOffset")] public const string DateTimeOffset = "DateTime";


        public static string GetTypeString(string dbTypeString, bool isNull)
        {
            var properties =
                (from property in
                     typeof (TypeMap).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase |
                                                BindingFlags.GetField)
                 let attributes =
                     property.GetCustomAttributes(typeof (DBTypeAttribute), true).OfType<DBTypeAttribute>().ToList()
                 where
                     attributes.Count > 0 &&
                     attributes.Any(t => t.DBType.Equals(dbTypeString, StringComparison.OrdinalIgnoreCase))
                 let attribute =
                     attributes.FirstOrDefault(t => t.DBType.Equals(dbTypeString, StringComparison.OrdinalIgnoreCase))
                 select property.GetRawConstantValue() as string)
                    .ToList();
            string tmp = properties.FirstOrDefault();
            if (string.IsNullOrEmpty(tmp))
                throw new InvalidCastException("Can not cast Db Type[" + dbTypeString + "] To .net based Type.");
            return isNull && !tmp.Contains("[]") ? (tmp + "?") : tmp;
        }
    }
}