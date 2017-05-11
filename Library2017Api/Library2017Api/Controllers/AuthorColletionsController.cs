using System;
using System.Collections.Generic;
using AutoMapper;
using Library.API.Entities;
using Library.API.Services;
using Library2017Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library2017Api.Controllers
{
    [Route("api/authorcollections")]
    public class AuthorColletionsController :Controller
    {
        private readonly ILibraryRepository libraryRepository;

        public AuthorColletionsController(ILibraryRepository libraryRepository)
        {
            this.libraryRepository = libraryRepository;
        }

        [HttpPost]
        public IActionResult CreateAuthors([FromBody] IEnumerable<AuthorDto> authorCollection)
        {
            if (authorCollection == null)
            {
                return BadRequest();
            }

            var authorEntities = Mapper.Map<ICollection<Author>>(authorCollection);

            foreach (var author in authorEntities)
            {
                libraryRepository.AddAuthor(author);
            }

            if (!libraryRepository.Save())
            {
                throw new Exception("Creating an author collection failed");
            }

            return Ok();
        }
    }
}