// import React, { useState, ChangeEvent, FormEvent } from "react";
import axios, { AxiosError } from "axios";
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";
import { toast } from "react-toastify";
import "./SalarySlipForm.css";
import { useState } from "react";

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

interface FormProps {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  handleApiResult: (data: any) => void;
}

interface FormValues {
  firstName: string;
  lastName: string;
  annualSalary: string;
  superRate: string;
  payPeriod: number;
}

const initialValues: FormValues = {
  firstName: "",
  lastName: "",
  annualSalary: "",
  superRate: "",
  payPeriod: -1,
};

const monthOptions = [
  { label: "January", value: 1 },
  { label: "February", value: 2 },
  { label: "March", value: 3 },
  { label: "April", value: 4 },
  { label: "May", value: 5 },
  { label: "June", value: 6 },
  { label: "July", value: 7 },
  { label: "August", value: 8 },
  { label: "September", value: 9 },
  { label: "October", value: 10 },
  { label: "November", value: 11 },
  { label: "December", value: 12 },
];

const validationSchema = Yup.object().shape({
  firstName: Yup.string()
    .required("First Name should not be empty")
    .min(2, "First Name must be at least 2 characters")
    .max(50, "First Name must not exceed 50 characters"),
  lastName: Yup.string()
    .required("Last Name should not be empty")
    .min(2, "Last Name must be at least 2 characters")
    .max(50, "Last Name must not exceed 50 characters"),
  annualSalary: Yup.number()
    .required("Annual Salary is required.")
    .min(1, "Annual Salary must be greater than 0.")
    .integer("Annual Salary must be an integer."),
  superRate: Yup.number()
    .required("Super Rate is required.")
    .moreThan(0, "Percentage value must be greater than zero")
    .max(100, "Percentage value must be at most 100"),
  payPeriod: Yup.number()
    .required("Pay Period is required.")
    .integer("Pay Period must be an integer.")
    .min(1, "Please select a valid month.")
    .max(12, "Please select a valid month."),
});

const FormComponent: React.FC<FormProps> = ({ handleApiResult }) => {
  const [loading, setLoading] = useState(false);
  const handleSubmit = async (values: FormValues) => {
    try {
      setLoading(true);
      // // Convert payPeriod to a number before sending the request
      const numericPayPeriod = parseInt(values.payPeriod.toString(), 10);
      const superRatePercentage = values.superRate.toString() + "%";

      const response = await axios.post(`${API_BASE_URL}/PaySlipGenerator`, {
        ...values,
        payPeriod: numericPayPeriod,
        superRate: superRatePercentage,
      });
      if (response.status == 200) {
        handleApiResult(response.data);
      } else {
        handleApiResult(null);
        toast.error("something went wrong");
      }
      setLoading(false);
    } catch (error) {
      setLoading(false);
      if (axios.isAxiosError(error)) {
        const axiosError = error as AxiosError;
        if (axiosError.response) {
          if (axiosError.response.status === 400) {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const errors = (axiosError.response.data as any).errors;
            const firstKey = Object.keys(errors)[0];
            const firstItem = errors[firstKey][0];
            toast.error(firstItem);
          }
          if (axiosError.response.status === 500) {
            toast.error("Something went wrong. Please try again later.");
          }
        } else if (axiosError.request) {
          toast.error("No response from server. Please try again later.");
        } else {
          toast.error("Something went wrong. Please try again later.");
        }
      } else {
        toast.error("Something went wrong. Please try again later.");
      }
      handleApiResult(null);
    }
  };

  return (
    <div className={`form-container ${loading ? "blur" : ""}`}>
      {loading && (
        <div className="loader-container">
          <div className="loader" />
        </div>
      )}
      <Formik
        initialValues={initialValues}
        validationSchema={validationSchema}
        onSubmit={handleSubmit}
      >
        {() => (
          <Form>
            <div>
              <label>First Name</label>
              <Field type="text" name="firstName" />
              <ErrorMessage name="firstName" component="div" />
            </div>
            <div>
              <label>Last Name</label>
              <Field type="text" name="lastName" />
              <ErrorMessage name="lastName" component="div" />
            </div>
            <div>
              <label>Annual Salary</label>
              <Field type="number" name="annualSalary" />
              <ErrorMessage name="annualSalary" component="div" />
            </div>
            <div>
              <label>Super Rate (%)</label>
              <Field type="text" name="superRate" />
              <ErrorMessage name="superRate" component="div" />
            </div>
            <div>
              <label>Pay Period</label>
              <Field as="select" name="payPeriod">
                <option disabled value="-1">
                  Select Pay Period
                </option>
                {monthOptions.map((option) => (
                  <option key={option.value} value={option.value}>
                    {option.label}
                  </option>
                ))}
              </Field>
              <ErrorMessage name="payPeriod" component="div" />
            </div>
            <button type="submit">Submit</button>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default FormComponent;
