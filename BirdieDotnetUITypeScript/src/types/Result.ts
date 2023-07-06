type Result<T, E> = 
    { success: true; value: T } | 
    { success: false; error: E };

export default Result;