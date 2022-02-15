using System;

// Importando Pacote (ControllerBase)
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Importando classe
using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Blog.Extensions;

namespace Blog.Controllers {

    [ApiController]
    public class CategoryController : ControllerBase {


        // pegar por Id
        // versionamento = v1 (versão 1)
        [HttpGet("v1/categories/{id:int}")]
        // O async retorna uma tarefa(Task)
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] BlogDataContext context) {

            try {

                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null) {
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));
                }
                return Ok(new ResultViewModel<Category>(category));

            } catch {

                return StatusCode(500, new ResultViewModel<Category>("Falha interna no servidor"));
            }


        }


        // Pegar todos
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext context) {

            try {

                var categories = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));

            } catch {

                return StatusCode(500, new ResultViewModel<List<Category>>("05X04 - Falha interna no servidor"));

            }

        }



        // Post
        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
            // CreateCategoryViewModel no luga de Category para simplificar a requisição
            // Entrada de dados diferente(model)
            [FromBody] EditorCategoryViewModel model,
            [FromServices] BlogDataContext context) {

            if (!ModelState.IsValid) {// Verificar se EditorCategoryViewModel esta valido
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
            }

            try {
                // gerando nova categoria
                var category = new Category {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                    Posts = null
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));

            } catch (DbUpdateException ex) {

                return StatusCode(500, new ResultViewModel<Category>("05XE9 - Não foi possível incluir a categoria"));

            } catch (Exception ex) {

                return StatusCode(500, new ResultViewModel<Category>("05X10 - Falha interna no servidor"));
            }


        }


        // Put
        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
           [FromBody] EditorCategoryViewModel model,
           [FromServices] BlogDataContext context) {

            try {

                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null) {
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));
                }

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));

            } catch (DbUpdateException ex) {

                return StatusCode(500, new ResultViewModel<Category>("05XE8 - Não foi possível alterar a categoria"));

            } catch (Exception ex) {

                return StatusCode(500, new ResultViewModel<Category>("05X11 - Falha interna no servidor"));
            }


        }


        // Delete
        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] BlogDataContext context) {

            try {

                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null) {
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));

                }

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));

            } catch (DbUpdateException ex) {

                return StatusCode(500, new ResultViewModel<Category>("05XE7 - Não foi possível excluir a categoria"));

            } catch (Exception ex) {

                return StatusCode(500, new ResultViewModel<Category>("05X12 - Falha interna no servidor"));
            }


        }

    }
}