using System;
using FluentResults;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace eAgendaWeb.WebApplication.Compartilhado.Apresentacao.Extensions;

public static class TempDataExtensions
{
    public static void AddErrorMessage(this ITempDataDictionary tempData, ResultBase result)
    {
        tempData["MensagemErro"] = result.Errors.First().Message;
    }

    public static void AddSuccessMessage(this ITempDataDictionary tempData, ResultBase result)
    {
        tempData["MensagemSucesso"] = result.Successes.First().Message;
    }
}
