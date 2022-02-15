

using System.ComponentModel.DataAnnotations;// Para usar Required

namespace Blog.ViewModels {

    public class EditorCategoryViewModel {

        // validações
        [Required(ErrorMessage = "O Nome é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo deve ter entre 3 e 40 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Slug é obrigatório")]
        public string Slug { get; set; }

    }
}