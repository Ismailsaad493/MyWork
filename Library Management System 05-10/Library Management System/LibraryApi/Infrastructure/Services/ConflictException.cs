namespace LibraryApi.Infrastructure.Services;

public sealed class ConflictException(string message) : Exception(message);
