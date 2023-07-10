/** A custom type for returning either a value or an error.
* @type {Result}
* @example
* // Successful result
* const successResult = { success: true, value: 42 };
* // Failed result
* const errorResult = { success: false, error: "Something went wrong" };
* @template T The type of the value.
* @template E The type of the error.
*/
type Result<T, E = undefined> = 
    { success: true; value: T } | 
    { success: false; error: E | undefined};

export default Result;