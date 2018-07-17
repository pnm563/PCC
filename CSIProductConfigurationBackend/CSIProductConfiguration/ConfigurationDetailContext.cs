using CSIProductConfigurationBackend.BusinessLogic;
using CSIProductConfigurationCommon;
using CSIProductConfigurationCommon.Enums;
using CSIProductConfigurationCommon.Models;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CSIProductConfigurationBackend.CSIProductConfiguration
{
    public class ConfigurationDetailContext : ContextBase
    {

        public List<ProcessingStackItem> ProcessingStack = new List<ProcessingStackItem>();
        List<Case> cases;
        List<CaseValue> caseValues;
        List<Condition> conditions;
        List<Constant> constants;
        List<Expression> expressions;
        String outputText = "";

        public ConfigurationDetail GetPricing(ConfigurationDetail configurationDetail)
        {

            outputText = "";

            CaseContext _cctx = new CaseContext();
            cases = _cctx.GetCases();
            CaseValueContext _cvctx = new CaseValueContext();
            caseValues = _cvctx.GetCaseValues();
            ConditionContext _cdctx = new ConditionContext();
            conditions = _cdctx.GetConditions();
            ConstantContext _cnctx = new ConstantContext();
            constants = _cnctx.GetConstants();
            ExpressionContext _exctx = new ExpressionContext();
            expressions = _exctx.GetExpressions();

            ProcessingStack.Clear();

            // Parse("${d:CostPlusMarkup} + ${d:MonthlyCharge}", configurationDetail);

            configurationDetail.IsError = false;
            try
            {

                foreach (ConfigurationTypeOutput output in configurationDetail.ConfigurationTypeOutputs)
                {
                    outputText += "Processing " + output.Action + "/" + output.ActionType + "<br/>";
                    output.Value = Process(output.Action, output.ActionType, configurationDetail);
                }
            }
            catch (Exception ex)
            {
                outputText += "ERROR : " + ex.Message + "<br/>";
                configurationDetail.IsError = true;
            }

            configurationDetail.OutputText = outputText;
            return configurationDetail;
        }

        int level = 0;

        public String Parse(String code, ConfigurationDetail configurationDetail, bool isExecute)
        {
            level++;
            outputText += SpaceLevel() + "Parsing Code " + code + "<br/>";
            
            // Check stack

            // Loop round, finding the placeholders

            String resultingCode = "";
            String originalCode = code;

            while (code.IndexOf("${") >= 0)
            {
                if (code.IndexOf("${") > 0)
                    resultingCode += code.Substring(0, code.IndexOf("${"));
                code = code.Substring(code.IndexOf("${") + 2);
                int endPos = code.IndexOf("}");
                if (code.IndexOf("${") >= 0 && code.IndexOf("${") < endPos)
                {
                    throw new Exception("Unterminated placeholder seen " + originalCode);
                }
                String placeHolder = code.Substring(0, endPos);
                code = code.Substring(endPos + 1);
                String name = placeHolder.Substring(2);

                switch (placeHolder.Substring(0, 1))
                {
                    case "d" :
                        if (! constants.Exists(x => x.Name.ToUpper().Equals(name.ToUpper())))
                            throw new Exception("Constant " + name + " cannot be found");
                        resultingCode += ProcessType(constants.Find(x => x.Name.ToUpper().Equals(name.ToUpper())).Value, constants.Find(x => x.Name.ToUpper().Equals(name.ToUpper())).ValueType);
                        break;
                    case "e":
                        if (!expressions.Exists(x => x.Name.ToUpper().Equals(name.ToUpper())))
                            throw new Exception("Expression " + name + " cannot be found");

                        Expression expression = expressions.Find(x => x.Name.ToUpper().Equals(name.ToUpper()));
                        if (!expression.IsProcessed)
                        {
                            outputText += SpaceLevel() + "Evaluating Expression " + name + "<br/>";
                            CheckStack(name, ActionType.ExpressionAction);
                            expression.Value = ProcessType(Parse(expression.Code, configurationDetail, true), expression.ValueType);
                            RemoveStack(name, ActionType.ExpressionAction);
                            // Execute code
                            expression.IsProcessed = true;
                            outputText += SpaceLevel() + "Expression Evaluated as " + expression.Value + "<br/>";
                        }
                        else
                            outputText += SpaceLevel() + "Using Expression " + name + " " + expression.Value + "<br/>";

                        resultingCode += expression.Value;
                        break;
                    case "q":
                        if (!conditions.Exists(x => x.Name.ToUpper().Equals(name.ToUpper())))
                            throw new Exception("Condition " + name + " cannot be found");

                        Condition condition = conditions.Find(x => x.Name.ToUpper().Equals(name.ToUpper()));
                        if (!condition.IsProcessed)
                        {
                            outputText += SpaceLevel() + "Evaluating Condition " + name + "<br/>";
                            CheckStack(name, ActionType.ConditionAction);
                            condition.Value = Parse(condition.Question, configurationDetail, true) == "1" ? true : false;
                            RemoveStack(name, ActionType.ConditionAction);
                            
                            condition.IsProcessed = true;
                        }
                        else
                        {
                            outputText += SpaceLevel() + "Using Condition " + name + " " + condition.Value + "<br/>";

                        }
                        if (condition.Value)
                        {
                            resultingCode += Process(condition.TrueActionName, condition.TrueActionType, configurationDetail);
                        }
                        else
                        {
                            resultingCode += Process(condition.FalseActionName, condition.FalseActionType, configurationDetail);
                        }
                        break;
                    case "c":
                        if (!cases.Exists(x => x.Name.ToUpper().Equals(name.ToUpper())))
                            throw new Exception("Case " + name + " cannot be found");

                        Case theCase = cases.Find(x => x.Name.ToUpper().Equals(name.ToUpper()));
                        if (!theCase.IsProcessed)
                        {
                            outputText += SpaceLevel() + "Evaluating Case " + name + "/" + theCase.ExpressionName + "<br/>";

                            String parameterValue = Parse("${p:" + theCase.ExpressionName + "}", configurationDetail, false);
                            CaseValue caseValue = null;

                            if (caseValues.Exists(x => x.CaseName.ToUpper().Equals(theCase.Name.ToUpper()) && (x.Value != null && x.Value.ToUpper().Equals(parameterValue.ToUpper()))))
                            {
                                caseValue = caseValues.Find(x => x.CaseName.ToUpper().Equals(theCase.Name.ToUpper()) && x.Value.ToUpper().Equals(parameterValue.ToUpper()));
                            }
                            else
                            {
                                if (caseValues.Exists(x => x.CaseName.ToUpper().Equals(theCase.Name.ToUpper()) && (x.Value == null || x.Value == "")))
                                {
                                    caseValue = caseValues.Find(x => x.CaseName.ToUpper().Equals(theCase.Name.ToUpper()) && (x.Value == null || x.Value == ""));
                                }
                                else
                                    throw new Exception("NO Case value or null entry " + parameterValue + " for case " + name);
                            }

                            theCase.Value = Process(caseValue.ActionName, caseValue.ActionType, configurationDetail);
                            // Execute question
                            theCase.IsProcessed = true;
                        }
                        else
                        {
                            outputText += SpaceLevel() + "Using Case " + name + " " + theCase.Value + "<br/>";

                        }
                        resultingCode += theCase.Value;
                        break;
                    case "o":
                        if (!configurationDetail.ConfigurationTypeOutputs.Exists(x => x.Name.ToUpper().Equals(name.ToUpper())))
                            throw new Exception("Output " + name + " cannot be found");

                        ConfigurationTypeOutput output = configurationDetail.ConfigurationTypeOutputs.Find(x => x.Name.ToUpper().Equals(name.ToUpper()));
                        if (!output.IsProcessed)
                        {
                            outputText += SpaceLevel() + "Evaluating Output " + name + "<br/>";
                            output.Value = Process(output.Action, output.ActionType, configurationDetail);
                            output.IsProcessed = true;
                            outputText += SpaceLevel() + "Output Evaluated as " + output.Value + "<br/>";
                        }
                        else
                            outputText += SpaceLevel() + "Using Output " + name + " " + output.Value + "<br/>";

                        resultingCode += output.Value;
                        break;
                    case "p":
                        if (!configurationDetail.ConfigurationParameterValues.Exists(x => x.ParameterName.ToUpper().Equals(name.ToUpper())))
                            throw new Exception("Parameter " + name + " cannot be found");

                        String value = configurationDetail.ConfigurationParameterValues.Find(x => x.ParameterName.ToUpper().Equals(name.ToUpper())).Value;
                        outputText += SpaceLevel() + "Using Parameter " + name + " " + value + "<br/>";
                        resultingCode += value;

                        break;
                }

            }

            resultingCode += code;

            if (isExecute)
            {

                outputText += SpaceLevel() + "Executing " + resultingCode + "<br/>";

                MethodInfo function = CreateFunction(resultingCode);
                var executeFunction = (Func<double>)Delegate.CreateDelegate(typeof(Func<double>), function);
                var result = executeFunction();

                outputText += SpaceLevel() + "Result " + result.ToString() + "<br/>";
                if (level <= 4)
                {
                    if (level == 1)
                        outputText += "<b style='color:red'>OUTPUT" + originalCode + " = " + result.ToString() + "</b><br />";
                    else if (level == 2)
                        outputText += "<b style='color:blue'>" + SpaceLevel() + originalCode + " = " + result.ToString() + "</b><br />";
                    else if (level == 3)
                        outputText += "<b style='color:green'>" + SpaceLevel() + originalCode + " = " + result.ToString() + "</b><br />";
                    else if (level == 4)
                        outputText += "<b style='color:purple'>" + SpaceLevel() + originalCode + " = " + result.ToString() + "</b><br />";
                }
                else
                    outputText += SpaceLevel() + originalCode + " = " + result.ToString() + "<br/>";
                level--;
                return result.ToString();
            }
            else
            {
                if (level <= 4)
                {
                    if (level == 1)
                        outputText += "<b style='color:red'>RETURNING OUTPUT" + originalCode + " = " + resultingCode + "</b><br />";
                    else if (level == 2)
                        outputText += "<b style='color:blue'>RETURNING OUTPUT" + SpaceLevel() + originalCode + " = " + resultingCode + "</b><br />";
                    else if (level == 3)
                        outputText += "<b style='color:green'>RETURNING OUTPUT" + SpaceLevel() + originalCode + " = " + resultingCode + "</b><br />";
                    else if (level == 4)
                        outputText += "<b style='color:purple'>RETURNING OUTPUT" + SpaceLevel() + originalCode + " = " + resultingCode + "</b><br />";
                }
                else
                    outputText += SpaceLevel() + "Returning " + originalCode + " = " + resultingCode + "<br/>";
                level--;
                return resultingCode;
            }

        }

        public String ProcessType(String value, AttributeType type)
        {
            if (type == AttributeType.FloatType && value.IndexOf(".") < 0)
                value += ".0";
            return value;
        }

        public String SpaceLevel()
        {
            String ret = "";
            for (int i = 0; i < level; ++i)
            {
                ret += "&nbsp;&nbsp;&nbsp;";
            }
            return ret;
        }

        public static MethodInfo CreateFunction(string function)
        {
            string code = @"
                using System;
            
                namespace UserFunctions
                {                
                    public class BinaryFunction
                    {                
                        public static double Function()
                        {
                            return func_xy;
                        }
                    }
                }
            ";

            string finalCode = code.Replace("func_xy", function);

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerResults results = provider.CompileAssemblyFromSource(new CompilerParameters(), finalCode);

            Type binaryFunction = results.CompiledAssembly.GetType("UserFunctions.BinaryFunction");
            return binaryFunction.GetMethod("Function");
        }


        public String Process(String action, ActionType actionType, ConfigurationDetail configurationDetail)
        {
            String placeHolder = "${";
            ActionType ?stackAction = null;

            switch (actionType)
            {
                case ActionType.CaseAction:
                    placeHolder += "c:";
                    stackAction = actionType;
                    break;
                case ActionType.ConditionAction:
                    placeHolder += "q:";
                    stackAction = actionType;
                    break;
                case ActionType.ConstantAction:
                    placeHolder += "d:";
                    break;
                case ActionType.ExpressionAction:
                    placeHolder += "e:";
                    stackAction = actionType;
                    break;
                default :
                    throw new Exception("Failed to find type " + actionType);
            }

            if (stackAction.HasValue)
                CheckStack(action, actionType);
            placeHolder += action + "}";
            String value = Parse(placeHolder, configurationDetail, true);
            if (stackAction.HasValue)
                RemoveStack(action, actionType);
            
            return value;
        }

        private void CheckStack(String name, ActionType actionType)
        {
            if (ProcessingStack.Exists(x => x.Name.ToUpper().Equals(name) && x.ActionType == actionType))
                throw new Exception("Action " + name + "/" + actionType + " has already been processed. Potential infinite loop");
            ProcessingStack.Add(new ProcessingStackItem() { Name = name, ActionType = actionType });
        }
        private void RemoveStack(String name, ActionType actionType)
        {
            ProcessingStack.RemoveAll((x => x.Name.ToUpper().Equals(name) && x.ActionType == actionType));
        }

    }
}