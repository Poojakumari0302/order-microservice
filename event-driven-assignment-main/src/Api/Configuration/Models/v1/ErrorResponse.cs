namespace Api.Configuration.Models.v1;

/// <summary>
///     Version 1 error response object.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    ///     Response error object.
    /// </summary>
    public ErrorModel Error { get; set; }
}