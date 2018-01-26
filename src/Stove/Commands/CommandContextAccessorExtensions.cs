﻿using System;

namespace Stove
{
    public static class CommandContextAccessorExtensions
    {
        public static string GetCorrelationIdOrEmpty(this IStoveCommandContextAccessor accessor)
        {
            if (accessor.CommandContext != null)
            {
                return accessor.CommandContext.CorrelationId;
            }

            return Guid.Empty.ToString();
        }
    }
}
