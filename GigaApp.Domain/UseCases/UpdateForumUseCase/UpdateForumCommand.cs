using GigaApp.Domain.Models;
using MediatR;

namespace GigaApp.Domain.UseCases.UpdateForumUseCase;

public record UpdateForumCommand(Forum Forum) : IRequest;