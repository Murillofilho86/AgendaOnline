using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //mapeamento...
using Projeto.DAL.Entities; //entidades

namespace Projeto.Web.Areas.Agenda.Models
{
    public class TarefaViewModelConsulta
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        [Display(Name = "Data de Início:")] //label
        public DateTime DataInicio { get; set; } //campo

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        [Display(Name = "Data de Término:")] //label
        public DateTime DataFim { get; set; } //campo


        //Listagem de Tarefas (Resultado da consulta)
        public List<Tarefa> Tarefas { get; set; }
    }
}