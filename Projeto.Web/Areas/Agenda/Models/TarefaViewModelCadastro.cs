using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //mapeamento
using Projeto.DAL.Entities.Types; //enums

namespace Projeto.Web.Areas.Agenda.Models
{
    public class TarefaViewModelCadastro
    {
        [Required(ErrorMessage = "Erro. Por favor, informe o titulo da tarefa.")]
        [Display(Name = "Titulo da Tarefa:")]
        public string Titulo { get; set; } //campo

        [Required(ErrorMessage = "Erro. Por favor, informe a descrição da tarefa.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição:")]
        public string Descricao { get; set; } //campo

        [Required(ErrorMessage = "Erro. Por favor, informe data/hora da tarefa.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data e Hora:")]
        public DateTime DataHora { get; set; } //campo

        [Required(ErrorMessage = "Erro. Por favor, selecione o status da tarefa.")]
        [Display(Name = "Status:")]
        public Status Status { get; set; } //campo

        [Required(ErrorMessage = "Erro. Por favor, selecione o tipo da tarefa.")]
        [Display(Name = "Tipo:")]
        public Tipo Tipo { get; set; } //campo
    }
}