using API.ViewModel;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
  [AllowAnonymous]
  [Route("api/[controller]")]
  [ApiController]
  public class EscolaridadeController : ControllerBase
  {
     private readonly IEscolaridadeRepository _escolaridadeRepository;

    public EscolaridadeController(IEscolaridadeRepository escolaridadeRepository)
    {
      _escolaridadeRepository = escolaridadeRepository;
    }

    [HttpGet]
    [Route("v1/listar")]
    public async Task<ResultViewModel> Listar()
    {
      try
      {
        var response = await _escolaridadeRepository.GetAll();
        return new ResultViewModel(false, "", response);
      }
      catch (Exception ex)
      {
        return new ResultViewModel(true, "Erro ao listar o usuário!", ex.Message);
      }

    }
  }
}
