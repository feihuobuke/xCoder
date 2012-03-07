using System;

namespace xCoder.Parser.xCode
{
    public delegate void StatementErrorHandler(Exception ex, string statment);
    public delegate void StatmentOutput(String output);
}