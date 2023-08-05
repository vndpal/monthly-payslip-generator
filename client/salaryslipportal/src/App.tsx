import React, { useState } from "react";
import FormComponent from "./components/SalarySlipForm";
import SalarySlipComponent from "./components/SalarySlipTable";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./App.css";

const App: React.FC = () => {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const [payslipData, setPayslipData] = useState<any>(null);

  // This function receives the payslip data from the API response and sets it in the state.
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const handleApiResult = (data: any) => {
    setPayslipData(data);
  };

  return (
    <>
      <ToastContainer />
      <h1>Pay Slip Generator</h1>
      <div className="container">
        <div className="form-component">
          <FormComponent handleApiResult={handleApiResult} />
        </div>
        {payslipData && <SalarySlipComponent payslipData={payslipData} />}
      </div>
    </>
  );
};

export default App;
