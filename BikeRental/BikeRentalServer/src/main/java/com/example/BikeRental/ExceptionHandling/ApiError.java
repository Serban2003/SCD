package com.example.BikeRental.ExceptionHandling;

import com.fasterxml.jackson.annotation.JsonInclude;
import java.time.OffsetDateTime;
import java.util.List;

@JsonInclude(JsonInclude.Include.NON_NULL)
public record ApiError(
        OffsetDateTime timestamp,
        int status,
        String error,
        String message,
        String path,
        List<String> errors
) {
    public static ApiError of(int status, String error, String message, String path, List<String> errors) {
        return new ApiError(OffsetDateTime.now(), status, error, message, path, errors);
    }
    public static ApiError of(int status, String error, String message, String path) {
        return of(status, error, message, path, null);
    }
}
