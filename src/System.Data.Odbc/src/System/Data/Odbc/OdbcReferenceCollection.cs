// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Data;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Diagnostics;

namespace System.Data.Odbc
{
    internal sealed class OdbcReferenceCollection : DbReferenceCollection
    {
        internal const int Closing = 0;
        internal const int Recover = 1;

        internal const int CommandTag = 1;

        override public void Add(object value, int tag)
        {
            base.AddItem(value, tag);
        }

        override protected void NotifyItem(int message, int tag, object value)
        {
            switch (message)
            {
                case Recover:
                    if (CommandTag == tag)
                    {
                        ((OdbcCommand)value).RecoverFromConnection();
                    }
                    else
                    {
                        Debug.Assert(false, "shouldn't be here");
                    }
                    break;
                case Closing:
                    if (CommandTag == tag)
                    {
                        ((OdbcCommand)value).CloseFromConnection();
                    }
                    else
                    {
                        Debug.Assert(false, "shouldn't be here");
                    }
                    break;
                default:
                    Debug.Assert(false, "shouldn't be here");
                    break;
            }
        }

        override public void Remove(object value)
        {
            base.RemoveItem(value);
        }
    }
}

