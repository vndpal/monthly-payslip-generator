import React from "react";
import "./SalarySlipTable.css";

interface SalarySlipProps {
  payslipData: {
    name: string;
    payPeriod: string;
    grossIncome: number;
    incomeTax: number;
    netIncome: number;
    super: number;
  };
}

const SalarySlipComponent: React.FC<SalarySlipProps> = ({ payslipData }) => {
  console.log(payslipData);
  return (
    <div className="salary-slip">
      <h2>Salary Slip</h2>
      <table>
        <tbody>
          <tr>
            <td className="label">Name:</td>
            <td>{payslipData.name}</td>
          </tr>
          <tr>
            <td className="label">Pay Period:</td>
            <td>{payslipData.payPeriod}</td>
          </tr>
          <tr>
            <td className="label">Gross Income:</td>
            <td>{payslipData.grossIncome.toFixed(2)}</td>
          </tr>
          <tr>
            <td className="label">Income Tax:</td>
            <td>{payslipData.incomeTax.toFixed(2)}</td>
          </tr>
          <tr>
            <td className="label">Net Income:</td>
            <td>{payslipData.netIncome.toFixed(2)}</td>
          </tr>
          <tr>
            <td className="label">Super:</td>
            <td>{payslipData.super.toFixed(2)}</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default SalarySlipComponent;
