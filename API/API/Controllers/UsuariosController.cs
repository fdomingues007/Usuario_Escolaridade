using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ViewModel;
using Domain.Entities;
using Domain.Interface;
using Infra.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEscolaridadeRepository _escolaridadeRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository, IEscolaridadeRepository escolaridadeRepository)
        {
            _usuarioRepository = usuarioRepository;
            _escolaridadeRepository = escolaridadeRepository;
        }

        [HttpPost]
        [Route("v1/adicionar")]
        public async Task<ResultViewModel> Adicionar([FromBody] Usuarios request)
        {
            try
            {
                Usuarios usuarios = new Usuarios(request.CodEscolaridade, request.Nome, request.SobreNome, request.Email, request.DtNascimento);
                var incluir = await _usuarioRepository.AddAsync(usuarios);
                return new ResultViewModel(true, "Inclusão do usuário realizado com sucesso! ", null);

            }
            catch (Exception ex)
            {
                return new ResultViewModel(true, "Erro ao incluir o usuário!", ex.Message);
            }
        }

        [HttpPut]
        [Route("v1/alterar")]
        public async Task<ResultViewModel> Alterar([FromBody] Usuarios request)
        {
            try
            {
                Usuarios usuarios = new Usuarios(request.IdUsuario, request.CodEscolaridade, request.Nome, request.SobreNome, request.Email, request.DtNascimento);
                var alterar = await _usuarioRepository.UpdateAsync(usuarios);
                return new ResultViewModel(true, "Alteração do usuário realizado com sucesso! ", null);
            }
            catch (Exception ex)
            {
                return new ResultViewModel(true, "Erro ao solicitar alteração de senha!", ex.Message);
            }
        }

        [HttpGet]
        [Route("v1/listar")]
        public async Task<ResultViewModel> Listar()
        {
            try
            {
                //Usuarios usuarios = new Usuarios();
                //var retornoef = await _usuarioRepository.GetAll();
                //return new ResultViewModel(false, "Consulta realizada com sucesso! ", retornoef);
                List<UsuarioListaView> usuariolistaview = new List<UsuarioListaView>();

                //using (var contexto = new EfContext())
                //{
                //    var listaUsuario = contexto.Usuarios.Include(x => x.Escolaridade).AsNoTracking().ToList();
                //    return new ResultViewModel(false, "Consulta realizada com sucesso! ", listaUsuario);
                //}
                return null;

            }
            catch (Exception ex)
            {
                return new ResultViewModel(true, "Erro ao listar o usuário!", ex.Message);
            }

        }

        [HttpDelete]
        [Route("v1/delete")]
        public async Task<ResultViewModel> Delete([FromBody] Usuarios request)
        {
            try
            {
                Usuarios usuarios = new Usuarios();
                usuarios.IdUsuario = request.IdUsuario;
                await _usuarioRepository.DeleteAsync(usuarios);
                return new ResultViewModel(true, "Exclusão do usuário realizado com sucesso! ", null);

            }
            catch (Exception ex)
            {
                return new ResultViewModel(true, "Erro ao excluir o usuário!", ex.Message);
            }
        }


    }
}