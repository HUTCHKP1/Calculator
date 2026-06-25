# ID521 Digital Numeracy Calculator

A command-line calculator implementing binary arithmetic, matrix operations, and number theory
as part of the ID521 Digital Numeracy assignment at Otago Polytechnic.

---

## Getting Started

1. Open the solution in **Visual Studio 2022**
2. Set `Calculator` as the startup project
3. Press **F5** to build and run
4. Type commands into the console

Type `HELP` for a command reference. Type `ESC` to exit. Type `CLR` to clear the screen.

---

## Command Reference

### Basic Arithmetic

Standard arithmetic is entered without spaces using the operators `+` `-` `*` `/` `%` `!`

```
5+3         8
10-2        8
3*4         12
10/2        5
10%3        1
5!          120
```

Division by zero and invalid factorial inputs (negative numbers, decimals) return descriptive
error messages rather than crashing.

---

### Binary Operations

All binary commands are prefixed with `BIN` followed by an operation keyword.

#### Addition and Subtraction

```
BIN ADDU 1010 0101      Unsigned binary addition
BIN ADDS 1010 0101      Signed binary addition (sign-extends inputs first)
BIN SUBU 1010 0101      Unsigned binary subtraction via two's complement
BIN SUBS 1010 0101      Signed binary subtraction via two's complement
```

#### Base Conversion

```
BIN CONVERT BIN 1010    Binary  to decimal and hexadecimal
BIN CONVERT HEX FF      Hex     to decimal and binary
BIN CONVERT DEC 42      Decimal to binary  and hexadecimal
```

#### Binary Coded Decimal

`BIN BCD` converts a decimal number to its BCD representation, where each digit is encoded
as an independent 4-bit group.

```
BIN BCD 123
0001 0010 0011
```

`BIN BCDA` adds two decimal numbers using BCD arithmetic, displaying the intermediate BCD
values and the result.

```
BIN BCDA 53 21
BCD A:  0101 0011
BCD B:  0010 0001
Result: 0111 0100
```

---

### Matrices

Matrices are 2x2 and named with single letters (`a`–`z`). Defined matrices persist across
commands within a session.

#### Defining a Matrix

```
mat a 1 2 3 4
```

Stores matrix `a` as:

```
[ 1  2 ]
[ 3  4 ]
```

#### Operations

| Command          | Description                              |
|------------------|------------------------------------------|
| `addMat a b`     | Element-wise addition of matrices a and b |
| `dotMat a b`     | Matrix dot product of a and b            |
| `scalMat 3 a`    | Multiply every element of a by scalar 3  |
| `detMat a`       | Calculate the determinant of a           |
| `invMat a`       | Calculate the inverse of a               |

#### Example Session

```
mat a 1 2 3 4
mat b 5 6 7 8

dotMat a b
[ 19  22 ]
[ 43  50 ]

detMat a
Determinant: -2

invMat a
[ -2   1    ]
[ 1.5  -0.5 ]
```

> Attempting to invert a singular matrix (determinant = 0) returns an error message.

---

### Number Theory

#### Primality Testing

Tests whether a given integer (up to 10,000) is prime using trial division up to the
square root of the number.

```
numPrime 97
97 is prime

numPrime 100
100 is not prime
```

#### Check Digits

Accepts a numeric string and automatically determines the barcode type from its length,
then calculates and appends the correct check digit.

```
numCheckDigit 03600029145      UPC-A   (11 digits)
numCheckDigit 400638133393     EAN-13  (12 digits)
numCheckDigit 978030640615     ISBN-13 (12 digits, prefix 978 or 979)
numCheckDigit 020161622        ISBN-10 (9 digits, check digit may be X)
```

| Input Length | Type            | Algorithm                          |
|--------------|-----------------|------------------------------------|
| 9 digits     | ISBN-10         | Descending weights 10–2, mod 11    |
| 11 digits    | UPC-A           | Alternating x3/x1, mod 10         |
| 12 digits    | EAN-13          | Alternating x1/x3, mod 10         |
| 12 digits, prefix 978 or 979 | ISBN-13 | Alternating x1/x3, mod 10 |

#### Linear Congruential Generator

Generates a pseudo-random number sequence using the formula:

```
Xi+1 = ( a * Xi + c ) mod m
```

```
numRand 5 3 1 16
X1 = ( 5 * 3 + 1 ) mod 16 = 0
X2 = ( 5 * 0 + 1 ) mod 16 = 1
X3 = ( 5 * 1 + 1 ) mod 16 = 6
```

The sequence runs until it returns to the original seed value, or 100 steps maximum.

---

### Utility Commands

| Command | Description             |
|---------|-------------------------|
| `HELP`  | Display command reference |
| `CLR`   | Clear the console screen |
| `ESC`   | Exit the calculator      |

---

## Error Handling

Invalid inputs return descriptive error messages rather than exceptions.

```
BIN ADDU 12abc 0101     Error: inputs must contain only 0s and 1s
BIN BCD 12.5            Error: input must be a positive whole number
invMat a                Error: matrix is singular (determinant = 0), cannot invert
detMat z                Error: matrix not found
10/0                    Undefined: Cannot divide by zero
-1!                     Factorial requires a non-negative integer
```

---

## Unit Tests

Two MSTest projects are included covering all implemented methods.

| Project                        | Classes Tested                                      |
|--------------------------------|-----------------------------------------------------|
| `BinaryTesting`                | `Binary` — Addition, Subtraction, Negate, PadSigned, PadUnsigned, DecimalToBCD, BCDAddition, IsValidBinary, IsValidDecimal |
| `MatrixNumberTheoryTesting`    | `Matrix` — Store, Find, Add, ScalarMultiply, DotProduct, Determinant, Inverse |
|                                | `NumberTheory` — IsPrime, UPCCheckDigit, EANCheckDigit, ISBN10CheckDigit |

Tests cover valid inputs, edge cases, and invalid inputs. Known limitations are documented
with intentional failing tests rather than silently omitted.

To run: open **Test Explorer** in Visual Studio and select **Run All**.

---

## Project Structure

```
Calculator/
    Binary.cs               Binary arithmetic and BCD
    Calculate.cs            Standard arithmetic operations
    Matrix.cs               2x2 matrix operations and storage
    NumberTheory.cs         Primality, check digits, LCG random number generation
    Program.cs              Input parsing and command routing

BinaryTesting/
    BinaryTests.cs          Unit tests for Binary class

MatrixNumberTheoryTesting/
    MatrixNumberTheoryTesting.cs    Unit tests for Matrix and NumberTheory classes

AI_Use_Documentation.docx  AI usage log, prompts, and critique
README.md                   This file
```

---

## References

- Binary string addition algorithm — https://www.geeksforgeeks.org/add-two-binary-strings/
- BCD addition — https://www.scaler.com/topics/bcd-addition/
- C# string Contains method — https://www.geeksforgeeks.org/c-sharp/c-sharp-string-contains-method/
- Checking array elements in C# — https://www.delftstack.com/howto/csharp/check-for-an-element-inside-an-array-in-csharp/

---

## AI Use

Claude (Anthropic, Sonnet 4.6) was used throughout this assignment to understand algorithms
and receive pseudocode guidance. All submitted code was written independently. Full details
including prompts, responses, and critique of AI output are documented in
`AI_Use_Documentation.docx`.

---

*ID521 Digital Numeracy | Otago Polytechnic | June 2026*
