﻿using Winium.Cruciatus.Exceptions;
using Winium.Cruciatus.Extensions;
using Winium.StoreApps.Common;

namespace Winium.Desktop.Driver.CommandExecutors
{
    internal class SelectDataGridCellExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var dataGridKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var column = int.Parse(this.ExecutedCommand.Parameters["COLUMN"].ToString());
            var row = int.Parse(this.ExecutedCommand.Parameters["ROW"].ToString());

            var dataGrid = this.Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToDataGrid();

            try
            {
                dataGrid.SelectCell(row, column);
            }
            catch (CruciatusException exception)
            {
                return this.JsonResponse(ErrorCodes.NoSuchElement, exception);
            }

            return this.JsonResponse();
        }
    }
}
