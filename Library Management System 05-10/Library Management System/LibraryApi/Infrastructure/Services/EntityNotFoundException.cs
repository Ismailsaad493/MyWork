namespace LibraryApi.Infrastructure.Services;

public sealed class EntityNotFoundException(string message) : Exception(message);
