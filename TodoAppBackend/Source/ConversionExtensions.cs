using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoAppBackend.Data;
using TodoAppShared;

namespace TodoAppBackend.Source
{
    public static class ConversionExtensions
    {
        public static TodoItem ToTodoItem(this TodoItemDTO dto)
        {
            if (dto == null)
                return null;

            return new TodoItem()
            {
                Id = dto.Id,
                UserID = dto.UserID,
                Title = dto.Title,
                IsCompleted = dto.IsCompleted,
                Description = dto.Description,
                CreateDate = dto.CreateDate,
                CompletedDate = dto.CompletedDate,
                LastEditedDate = dto.LastEditedDate
            };
        }
    }
}