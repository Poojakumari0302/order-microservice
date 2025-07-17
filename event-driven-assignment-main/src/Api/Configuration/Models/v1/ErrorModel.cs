namespace Api.Configuration.Models.v1;

/// <summary>
///     Version 1 error object model.
/// </summary>
public class ErrorModel
{
    /// <summary>
    ///     Error category provides a human readable identifier for the error.
    /// </summary>
    public string Category { get; set; }
    /// <summary>
    ///     Error message provides more information regarding the error.
    /// </summary>
    public string Message { get; set; }
}