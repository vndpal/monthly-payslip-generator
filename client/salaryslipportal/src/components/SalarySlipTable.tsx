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
            <td>
              <CurrencyDisplay value={payslipData.grossIncome} />
            </td>
          </tr>
          <tr>
            <td className="label">Income Tax:</td>
            <td>
              <CurrencyDisplay value={payslipData.incomeTax} />
            </td>
          </tr>
          <tr>
            <td className="label">Net Income:</td>
            <td>
              <CurrencyDisplay value={payslipData.netIncome} />
            </td>
          </tr>
          <tr>
            <td className="label">Super:</td>
            <td>
              <CurrencyDisplay value={payslipData.super} />
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

interface CurrencyDisplayProps {
  value: number;
}

const CurrencyDisplay: React.FC<CurrencyDisplayProps> = ({ value }) => {
  const formattedValue = value.toLocaleString("en-US", {
    style: "currency",
    currency: "USD",
  });

  return <span>{formattedValue}</span>;
};

export default SalarySlipComponent;
