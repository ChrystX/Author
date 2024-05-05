using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly AuthorsService _authorsService;

    public AuthorsController(AuthorsService authorsService) =>
        _authorsService = authorsService;

    [HttpGet]
    public async Task<List<Author>> Get() =>
        await _authorsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Author>> Get(string id)
    {
        var author = await _authorsService.GetAsync(id);

        if (author is null)
        {
            return NotFound();
        }

        return author;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Author newAuth)
    {
        await _authorsService.CreateAsync(newAuth);

        return CreatedAtAction(nameof(Get), new { id = newAuth.Id }, newAuth);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Author updatedAuth)
    {
        var author = await _authorsService.GetAsync(id);

        if (author is null)
        {
            return NotFound();
        }

        updatedAuth.Id = author.Id;

        await _authorsService.UpdateAsync(id, updatedAuth);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var author = await _authorsService.GetAsync(id);

        if (author is null)
        {
            return NotFound();
        }

        await _authorsService.RemoveAsync(id);

        return NoContent();
    }
}