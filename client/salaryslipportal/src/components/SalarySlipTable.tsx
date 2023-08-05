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
            <td>{payslipData.grossIncome}</td>
          </tr>
          <tr>
            <td className="label">Income Tax:</td>
            <td>{payslipData.incomeTax}</td>
          </tr>
          <tr>
            <td className="label">Net Income:</td>
            <td>{payslipData.netIncome}</td>
          </tr>
          <tr>
            <td className="label">Super:</td>
            <td>{payslipData.super}</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default SalarySlipComponent;
