# NucleicAcidAnalysis
염기서열 분석 라이브러리 (Compiler-compiler 연습용)

## Antlr4 Grammer Code-Generate

* Java 11 필요
* [antlr-4.12.0-complete.jar] (https://www.antlr.org/download/antlr-4.12.0-complete.jar)

### 실행 방법

java -jar antlr-4.12.0-complete.jar -Dlanguage=CSharp -package NucleicAcidAnalyzer.NucleicAcid.Compiler -visitor $NucleicAcidAnalyzerProjectDir\NucleicAcid\Compiler\NucleicAcidCompiler.g4